using DMSkin;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.YK;
using HLACommonView.Model;
using HLACommonView.Views;
using HLACommonView.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Xindeco.Device.Model;
using System.IO.Ports;
using System.Text.RegularExpressions;
using Impinj.OctaneSdk;
using System.Configuration;

namespace HLAWriteTagChannelMachine
{
    public partial class InventoryForm : CommonInventoryFormIMP
    {
        ImpinjReader mReader = new ImpinjReader();
        const ushort EPC_OP_ID = 123;
        int mWriteTagCount = 0;
        int mWriteSuccessTagCount = 0;
        int mHuTotalCount = 0;
        int mTotalTags = 0;
        public InventoryForm()
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);
        }
        private void InitView()
        {
            Invoke(new Action(() => {
                lblPlc.Text = "连接中...";
                lblReader.Text = "连接中...";
                lblWorkStatus.Text = "未开始工作";
                comboBox1_writeMode.SelectedIndex = 0;
                textBox2_curWriteEPC.Enabled = false;
            }));
        }
        int getWriteMode()
        {
            return comboBox1_writeMode.SelectedIndex;
        }
        bool initReader()
        {
            bool re = false;
            try
            {
                string mReaderIp = ConfigurationManager.AppSettings["ReaderIp"];
                mReader.Connect(mReaderIp);
                mReader.TagsReported += OnTagsReported;
                mReader.TagOpComplete += OnTagOpComplete;
                Settings settings = mReader.QueryDefaultSettings();

                settings.SearchMode = (SearchMode)int.Parse(ConfigurationManager.AppSettings["SearchMode"].ToString());
                settings.Session = (ushort)int.Parse(ConfigurationManager.AppSettings["Session"].ToString());
                double power = double.Parse(ConfigurationManager.AppSettings["AntennaPower"].ToString());
                List<AntennaConfig> ac = settings.Antennas.AntennaConfigs;
                foreach(var v in ac)
                {
                    v.IsEnabled = true;
                    v.TxPowerInDbm = power;
                }
                settings.Antennas.AntennaConfigs = ac;
                mReader.ApplySettings(settings);
                re = true;
            }
            catch (Exception)
            {
                re = false;
            }

            return re;
        }
        void OnTagsReported(ImpinjReader sender, TagReport report)
        {
            if (!isInventory) return;

            foreach (Tag tag in report.Tags)
            {
                string epc = tag.Epc.ToHexString();
                if (Regex.IsMatch(epc,textBox1_sourceEpcModel.Text.Trim()) && !epcList.Contains(epc))
                {
                    lastReadTime = DateTime.Now;
                    epcList.Add(epc);
                    Invoke(new Action(() =>
                    {
                        label_readTags.Text = epcList.Count.ToString();
                    }));
                }
            }
        }
        void saveWriteEpc(string epc)
        {
            string sql = string.Format("update wt set INFO='{0}' WHERE ID=0", epc);
            int af = SqliteDBHelp.ExecuteSql(sql);
            if(af>=1)
            {

            }
            else
            {
                sql = string.Format("insert into wt (ID,INFO) values (0,'{0}')", epc);
                SqliteDBHelp.ExecuteSql(sql);
            }
        }
        string restoreWriteEpc()
        {
#if DEBUG
            return "123456789012345678901234";
#endif
            string re = "";
            try
            {
                string sql = string.Format("select INFO from wt where ID=0");
                re = SqliteDBHelp.GetValue(sql).ToString();
            }
            catch(Exception)
            {
                re = "";
            }
            return re;
        }
        void OnTagOpComplete(ImpinjReader reader, TagOpReport report)
        {
            foreach (TagOpResult result in report)
            {
                if (result is TagWriteOpResult)
                {
                    TagWriteOpResult writeResult = result as TagWriteOpResult;
                    if (writeResult.OpId == EPC_OP_ID)
                    {
                        mWriteTagCount++;
                        if (writeResult.Result == WriteResultStatus.Success)
                        {
                            mWriteSuccessTagCount++;
                        }
                        Invoke(new Action(() =>
                        {
                            label11_writeTag.Text = mWriteSuccessTagCount.ToString();
                        }));

                        if (epcList.Count == mWriteTagCount)
                        {
                            bool re = mWriteSuccessTagCount == mWriteTagCount;
                            mReader.Stop();
                            SetInventoryResult(re?1:3);
                            playSound(re);
                            Invoke(new Action(() =>
                            {
                                dataGridView1_msg.Rows.Insert(0, re?"写入成功":"部分写入失败，请重新投");
                                if(!re)
                                {
                                    dataGridView1_msg.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                                }
                                if (re)
                                {
                                    textBox3_totalHuCount.Text = (++mHuTotalCount).ToString();
                                }
                                mTotalTags += mWriteSuccessTagCount;
                                textBox4_totalwriteEpcCount.Text = mTotalTags.ToString();
                            }));
                        }
                    }
                }
            }

        }
        private void InventoryForm_Load(object sender, EventArgs e)
        {
            InitView();

            btnStart.Enabled = false;
            Thread thread = new Thread(new ThreadStart(() => {
                ShowLoading("正在连接PLC...");
                if (ConnectPlc())
                    Invoke(new Action(() => { lblPlc.Text = "正常"; lblPlc.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblPlc.Text = "异常"; lblPlc.ForeColor = Color.OrangeRed; }));

                /*
                ShowLoading("正在连接条码扫描枪...");
                ConnectBarcode();
                */

                ShowLoading("正在连接读写器...");
                if (initReader())
                    Invoke(new Action(() => { lblReader.Text = "正常"; lblReader.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblReader.Text = "异常"; lblReader.ForeColor = Color.OrangeRed; }));

                Invoke(new Action(() =>
                {
                    btnStart.Enabled = true;
                    textBox2_curWriteEPC.Text = restoreWriteEpc();
                }));
                HideLoading();

            }));

            thread.IsBackground = true;
            thread.Start();
        }
        private void Start()
        {
            btnStart.Enabled = false;
            btnPause.Enabled = true;

#if DEBUG
            StartInventory();
#endif
        }
        private void Pause()
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;

            if (isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "停止扫描";
                }));
                isInventory = false;
                mReader.Stop();
                SetInventoryResult(1);
            }
        }
        public override void StartInventory()
        {
            if (!isInventory)
            {
                Invoke(new Action(() =>
                {
                    dataGridView1_msg.Rows.Clear();
                    lblWorkStatus.Text = "开始扫描";
                    if (btnStart.Enabled)
                    {
                        Start();
                    }
                    label_readTags.Text = "0";
                    label11_writeTag.Text = "0";
                }));

                mWriteTagCount = 0;
                mWriteSuccessTagCount = 0;
                SetInventoryResult(0);
                epcList.Clear();

                mReader.Start();
                isInventory = true;
                lastReadTime = DateTime.Now;
            }
        }
        void playSound(bool re)
        {
            try
            {
                if (re)
                {
                    AudioHelper.Play(".\\Res\\success.wav");
                }
                else
                {
                    AudioHelper.Play(".\\Res\\fail.wav");
                }
            }
            catch (Exception)
            { }
        }
        bool checkRe(out string msg)
        {
            bool re = true;
            msg = "";

            if(epcList.Count == 0)
            {
                msg = "没有扫描到EPC";
                re = false;
            }

            if (getWriteMode() == 1 && string.IsNullOrEmpty(textBox2_curWriteEPC.Text.Trim()))
            {
                msg = "写入EPC不能为空";
                re = false;
            }

            if(string .IsNullOrEmpty(textBox1_password.Text.Trim()))
            {
                msg = "写入密码不能为空";
                re = false;
            }

            if(checkBox2_xianggui.Checked)
            {
                string xianggui = textBox1_xianggui.Text.Trim();
                int xg = 0;
                int.TryParse(xianggui, out xg);
                if(xg!=0 && epcList.Count!=xg)
                {
                    msg = "箱规不符";
                    re = false;
                }
            }

            return re;
        }
        public override void StopInventory()
        {
            if (isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "停止扫描";
                }));
                isInventory = false;

                string msg = "";
                if(!checkRe(out msg))
                {
                    mReader.Stop();
                    playSound(false);
                    SetInventoryResult(1);
                    Invoke(new Action(() =>
                    {
                        dataGridView1_msg.Rows.Insert(0, msg);
                        dataGridView1_msg.Rows[0].DefaultCellStyle.BackColor = Color.Red;
                    }));
                    return;
                }
                if (getWriteMode() == 1)
                {
                    //递增模式
                    writeEpcSeqMode();
                }
                else
                {
                    //修改源epc模式
                    writeEpcWithSrc();
                }
            }
        }
        void writeEpcWithSrc()
        {
            try
            {
                foreach (var currentEpc in epcList)
                {
                    TagOpSequence seq = new TagOpSequence();
                    seq.TargetTag.MemoryBank = MemoryBank.Epc;
                    seq.TargetTag.BitPointer = BitPointers.Epc;
                    seq.TargetTag.Data = currentEpc;

                    string newEpc = getNewEpc(currentEpc);

                    TagWriteOp writeEpc = new TagWriteOp();
                    writeEpc.AccessPassword = TagData.FromHexString(textBox1_password.Text.Trim());
                    writeEpc.Id = EPC_OP_ID;
                    // Write to EPC memory
                    writeEpc.MemoryBank = MemoryBank.Epc;
                    // Specify the new EPC data
                    writeEpc.Data = TagData.FromHexString(newEpc);
                    // Starting writing at word 2 (word 0 = CRC, word 1 = PC bits)
                    writeEpc.WordPointer = WordPointers.Epc;

                    // Add this tag write op to the tag operation sequence.
                    seq.Ops.Add(writeEpc);
                    mReader.AddOpSequence(seq);
                }
            }
            catch (Exception)
            {

            }
        }
        string getNewEpc(string epc)
        {
            if (getWriteMode() == 1)
            {
                string re = textBox2_curWriteEPC.Text.Trim();
                return re;
            }
            else
            {
                return epc.Substring(0, 16) + "0" + epc.Substring(17);
            }
        }
        void writeEpcSeqMode()
        {
            try
            {
                foreach(var currentEpc in epcList)
                {
                    TagOpSequence seq = new TagOpSequence();
                    seq.TargetTag.MemoryBank = MemoryBank.Epc;
                    seq.TargetTag.BitPointer = BitPointers.Epc;
                    seq.TargetTag.Data = currentEpc;
                    
                    string newEpc = getNewEpc(currentEpc);

                    TagWriteOp writeEpc = new TagWriteOp();
                    writeEpc.AccessPassword = TagData.FromHexString(textBox1_password.Text.Trim());
                    writeEpc.Id = EPC_OP_ID;
                    
                    // Write to EPC memory
                    writeEpc.MemoryBank = MemoryBank.Epc;
                    // Specify the new EPC data
                    writeEpc.Data = TagData.FromHexString(newEpc);
                    // Starting writing at word 2 (word 0 = CRC, word 1 = PC bits)
                    writeEpc.WordPointer = WordPointers.Epc;

                    // Add this tag write op to the tag operation sequence.
                    seq.Ops.Add(writeEpc);
                    mReader.AddOpSequence(seq);

                    if(!selfAddCurEpc())
                    {
                        mReader.Stop();
                        SetInventoryResult(1);
                        playSound(false);
                        Invoke(new Action(() =>
                        {
                            dataGridView1_msg.Rows.Insert(0, "写入失败");
                            dataGridView1_msg.Rows[0].DefaultCellStyle.BackColor = Color.Red;
                        }));

                        break;
                    }

                    saveWriteEpc(textBox2_curWriteEPC.Text.Trim());
                }
            }
            catch(Exception)
            {

            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
            openMachineCommon();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
            closeMachineCommon();
        }

        private bool selfAddCurEpc()
        {
            string curEpc = textBox2_curWriteEPC.Text.Trim();

            string ser = strinc(curEpc.Substring(14, 5), 16);
            if (ser.Length != 5)
            {
                return false;
            }

            this.Invoke(new System.Action(() =>
            {
                textBox2_curWriteEPC.Text = curEpc.Substring(0, 14) + ser + curEpc.Substring(19, 5);
            }));

            return true;
        }
        private int Asc(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }

        }

        private string Chr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }
        private string strinc(string s1, int hex)
        {
            string str, newstr;
            int index = 0;
            int newInt;
            str = s1.Trim().Replace(" ", "");
            str = str.ToUpper();
            if (hex == 10)
            {
                for (int i = str.Length - 1; i > 0; i--)
                {
                    if (str[i] != (char)57)
                    {
                        index = i;
                        break;
                    }
                }
                newInt = Convert.ToInt32(str.Substring(index)) + 1;
                str = str.Substring(0, index) + newInt.ToString();
            }
            else if (hex == 16)
            {
                newstr = "";
                for (int i = str.Length - 1; i > 0; i--)
                {
                    if (str[i] == (char)57)
                    {
                        newstr = "A";
                        index = i;
                        break;
                    }
                    else if (str[i] != (char)70)
                    {
                        newstr = Chr(Asc(str.Substring(i, 1)) + 1);
                        index = i;
                        break;
                    }
                }
                str = str.Substring(0, index) + newstr + str.Substring(index + 1).Replace("F", "0");
            }

            return str;
        }

        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseWindow();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox1_password.PasswordChar = '\0';
            }
            else
            {
                textBox1_password.PasswordChar = '*';
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1_xianggui.Enabled = checkBox2_xianggui.Checked;
        }

        private void comboBox1_writeMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (getWriteMode() == 1)
            {
                textBox2_curWriteEPC.Enabled = true;
            }
            else
            {
                textBox2_curWriteEPC.Enabled = false;
            }
        }
    }
}
