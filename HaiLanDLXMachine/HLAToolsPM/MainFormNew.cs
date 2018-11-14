using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLATools.Utils;
using HLACommonLib.Model.ENUM;
using HLATools.Models;
using HLACommonLib.Model.PK;
using HLACommonView.Views.Dialogs;
using HLACommonLib.DAO;
using UARTRfidLink.Exparam;
using UARTRfidLink.Extend;
using HLACommonView.Views;

namespace HLATools
{
    public partial class MainFormNew : CommonPMInventoryForm
    {
        private ReceiveType receiveType = ReceiveType.交货单收货;

        public MainFormNew()
        {
            InitializeComponent();
            InitDevice(SysConfig.ReaderComPort, SysConfig.ReaderPower);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.tabControl.SelectTab(0);

            cboType.SelectedIndex = 1;

            Thread initThread = new Thread(new ThreadStart(() =>
            {
                

                bool closed = false;
                ShowLoading("正在下载物料主数据...");
                materialList = LocalDataService.GetMaterialInfoList();
                if (materialList == null || materialList.Count <= 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        HideLoading();
                        MetroMessageBox.Show(this, "未下载到物料主数据，请检查网络情况", "提示");

                        closed = true;
                        Close();
                    }));

                }
                if (closed)
                    return;

                ShowLoading("正在下载吊牌数据...");
                hlaTagList = LocalDataService.GetAllRfidHlaTagList();

                if (hlaTagList == null || hlaTagList.Count <= 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        HideLoading();
                        MetroMessageBox.Show(this, "未下载到吊牌主数据，请检查网络情况", "提示");

                        closed = true;
                        Close();
                    }));
                }

                if (closed)
                    return;

                if(SysConfig.IsTest)
                {
                    HideLoading();
                    return;
                }

                //连接读写器
                ShowLoading("正在连接读写器...");
                if (!ConnectReader())
                {
                    this.Invoke(new Action(() =>
                    {
                        HideLoading();
                        MetroMessageBox.Show(this, "连接读写器失败,请检查设备是否连接", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        closed = true;
                        Close();
                    }));
                }

                if (closed)
                    return;

                HideLoading();

                StartInventory();
            }));
            initThread.IsBackground = true;
            initThread.Start();
        }
        public override void StopInventory()
        {
            if (!isInventory)
                return;

            try
            {
                this.isInventory = false;
                base.StopInventory();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        public override void StartInventory()
        {
            if (isInventory)
                return;

            try
            {
                base.StartInventory();
                lastReadTime = DateTime.Now;
                isInventory = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        void check(string epc)
        {
            try
            {
                TagDetailInfo tag = GetTagDetailInfoByEpc(epc);
                string curTab = tabControl.SelectedTab.Name.Trim();
                if (curTab == "checkEpc")
                {
                    Invoke(new Action(() =>
                    {
                        if (tag == null)
                        {
                            grid.Rows.Insert(0, "", "", "", "", epc, "EPC未注册");
                            grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                        }
                        else
                        {
                            grid.Rows.Insert(0, "", tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, epc, "");
                        }

                        lblCount.Text = epcList.Count.ToString();
                    }));
                }
                if (curTab == "deStoreCheck")
                {
                    Invoke(new Action(() =>
                    {
                        CDeliverStoreQuery re = SAPDataService.getDeliverStore(epc);
                        metroGrid1.Rows.Add(re.store, re.mtr, re.bar, re.shipDate, re.hu, re.msg);
                    }));
                }
                if (curTab == "storeReturnCheck")
                {
                    Invoke(new Action(() =>
                    {
                        if (tag != null)
                        {
                            CStoreQuery sq = SAPDataService.getStoreFromSAP(tag.EPC);
                            metroGrid2.Rows.Insert(0, grid.Rows.Count, tag.EPC, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, tag.PUT_STRA, sq.barcd, sq.storeid, sq.hu, sq.pxqty_fh, sq.flag, sq.equip_hla, sq.loucheng, sq.date, sq.time);
                        }
                        else
                        {
                            metroGrid2.Rows.Insert(0, grid.Rows.Count, "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册");
                            grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
            }
        }
        public override void reportEpc(string epc)
        {
            if (!isInventory || string.IsNullOrEmpty(epc)) return;
            if (!epcList.Contains(epc))
            {
                epcList.Add(epc);
                check(epc);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseWindow();
        }

        private void txtHU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //回车
                queryHuErrorInfo();
            }
        }

        private void queryHuErrorInfo()
        {
            if (string.IsNullOrWhiteSpace(this.txtHU.Text))
            {
                MetroMessageBox.Show(this, "请输入箱码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            receiveType = (ReceiveType)Enum.Parse(typeof(ReceiveType), cboType.Text);
            this.txtHU.Enabled = false;
            this.btnBarcode.Enabled = false;
            grid2.Rows.Clear();
            ShowLoading("正在加载箱异常数据...");
            Thread thread = new Thread(new ThreadStart(() =>
            {
                //加载当前箱码下EPC未注册和商品已扫描的标签
                string hu = txtHU.Text.Trim();
                if (string.IsNullOrEmpty(hu))
                    return;
                DataTable dtError = LocalDataService.GetErrorEpcByHU(hu, receiveType);
                DataTable all = LocalDataService.GetAllDistinctEpcByHU(hu, receiveType);

                foreach (DataRow row in dtError.Rows)
                {
                    //加载错误为商品已扫描的epc
                    string epc = row["EPC_SER"].ToString();
                    TagDetailInfo tdi = GetTagDetailInfoByEpc(epc);
                    this.Invoke(new Action(() =>
                    {
                        if (tdi == null)
                        {

                            grid2.Rows.Insert(0,
                                row["EPC_SER"].ToString(),
                                "", "", "", row["HU"].ToString(), "不在本单;商品已扫描", "否");
                        }
                        else
                        {
                            grid2.Rows.Insert(0,
                                row["EPC_SER"].ToString(),
                                tdi.ZSATNR, tdi.ZCOLSN, tdi.ZSIZTX, row["HU"].ToString(), "商品已扫描", "否");
                        }
                    }));

                }

                foreach (DataRow row in all.Rows)
                {
                    string epc = row["EPC_SER"].ToString();
                    TagDetailInfo tdi = GetTagDetailInfoByEpc(epc);
                    if (tdi == null)
                    {
                        //不在本单
                        bool exist = false;
                        foreach (DataGridViewRow dgvrow in grid2.Rows)
                        {
                            if (dgvrow.Cells["EPC"].Value.ToString().Trim() == epc)
                            {
                                exist = true;
                            }
                        }
                        if (!exist)
                        {
                            this.Invoke(new Action(() =>
                            {
                                grid2.Rows.Insert(0,
                                row["EPC_SER"].ToString(),
                                "", "", "", row["HU"].ToString(), "不在本单", "否");
                            }));
                        }
                    }
                }

                this.Invoke(new Action(() =>
                {
                    this.txtHU.Enabled = true;
                    this.btnBarcode.Enabled = true;
                    this.txtHU.Focus();
                    HideLoading();
                    if (grid2.Rows.Count <= 0)
                    {
                        MetroMessageBox.Show(this, "当前箱没有异常信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        UnselectGrid2Rows();
                    }
                }));
            }));
            thread.IsBackground = true;
            thread.Start();
        }

        private void UnselectGrid2Rows()
        {
            foreach (DataGridViewRow row in grid2.Rows)
                row.Selected = false;
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            queryHuErrorInfo();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            epcList.Clear();
        }


        private void grid2_SelectionChanged(object sender, EventArgs e)
        {
            UnselectGrid2Rows();
        }

        private void txtShortHU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //回车
            }
        }

        private void btnQueryShortPick_Click(object sender, EventArgs e)
        {
            //清空列表
            this.Invoke(new Action(() =>
            {
                gridShort.Rows.Clear();
            }));

            if (string.IsNullOrWhiteSpace(txtShortHU.Text.Trim()))
            {
                MetroMessageBox.Show(this, "箱码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Thread thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    ShowLoading("正在下载短拣信息...");
                    DataSet ds = LocalDataService.GetShortPickHuInfo(txtShortHU.Text.Trim());
                    if (ds != null && ds.Tables.Count == 2)
                    {
                        DataTable dt1 = ds.Tables[0];
                        DataTable dt2 = ds.Tables[1];
                        List<ShortPickBoxInfo> list = new List<ShortPickBoxInfo>();
                        if (dt2.Rows.Count <= 0 || dt1.Rows.Count <= 0)
                        {
                            HideLoading();
                            Invoke(new Action(() =>
                            {
                                MetroMessageBox.Show(this, "该箱没有短拣信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }));

                            return;
                        }

                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt1.Rows)
                            {
                                ShortPickBoxInfo item = new ShortPickBoxInfo();
                                item.HU = row["HU"] != null ? row["HU"].ToString() : "";
                                item.PICK_TASK = row["PICK_TASK"] != null ? row["PICK_TASK"].ToString() : "";
                                item.PICK_TASK_ITEM = row["PICK_TASK_ITEM"] != null ? row["PICK_TASK_ITEM"].ToString() : "";
                                item.MATNR = row["PRODUCTNO"] != null ? row["PRODUCTNO"].ToString() : "";
                                item.QTY = row["QTY"] != null ? int.Parse(row["QTY"].ToString()) : 0;
                                var temp = list.FirstOrDefault(o => o.PICK_TASK == item.PICK_TASK);
                                if (temp == null)
                                    item.ISLAST = LocalDataService.CountUnScanDeliverBox(item.PICK_TASK) == 1 ? "是" : "否";
                                else
                                    item.ISLAST = temp.ISLAST;
                                MaterialInfo mi = materialList.FirstOrDefault(i => i.MATNR == item.MATNR);
                                item.ZCOLSN = mi != null ? mi.ZCOLSN : "";
                                item.ZSATNR = mi != null ? mi.ZSATNR : "";
                                item.ZSIZTX = mi != null ? mi.ZSIZTX : "";

                                list.Add(item);
                            }
                        }
                        //start edit by wuxw 统计实发数量，一箱存在多下架单，且不同下架单有相同MATNR，先填满尾箱，再填非尾箱
                        list = list.OrderByDescending(o => o.ISLAST).ThenBy(o => o.PICK_TASK).ThenBy(o => o.MATNR).ThenBy(o => o.QTY).ToList();
                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt2.Rows)
                            {
                                string matnr = row["MATNR"] != null ? row["MATNR"].ToString() : "";
                                int isAdd = row["IsAdd"] != null ? int.Parse(row["IsAdd"].ToString()) : 0;
                                if (isAdd != 1)
                                {
                                    //判断所属产品编码是否有属于尾箱且实发数量小于应发数量的记录
                                    var item = list.FirstOrDefault(o => o.MATNR == matnr && o.ISLAST == "是" && o.RQTY < o.QTY);
                                    if (item != null)
                                    {
                                        item.RQTY++;
                                        if (string.IsNullOrEmpty(item.ZCOLSN))
                                        {
                                            item.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                            item.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                            item.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                        }
                                    }
                                    else
                                    {
                                        //判断所属产品编码是否有不是尾箱且实发数量小于应发数量的记录
                                        var tItem = list.FirstOrDefault(o => o.MATNR == matnr && o.ISLAST == "否" && o.RQTY < o.QTY);
                                        if (tItem != null)
                                        {
                                            tItem.RQTY++;
                                            if (string.IsNullOrEmpty(tItem.ZCOLSN))
                                            {
                                                tItem.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                                tItem.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                                tItem.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                            }
                                        }
                                        else
                                        {
                                            //获取所属产品编码最后一条记录，看是否存在
                                            var lastItem = list.LastOrDefault(o => o.MATNR == matnr);
                                            if (lastItem != null)
                                            {
                                                lastItem.RQTY++;
                                                if (string.IsNullOrEmpty(lastItem.ZCOLSN))
                                                {
                                                    lastItem.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                                    lastItem.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                                    lastItem.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                                }
                                            }
                                            else
                                            {
                                                //找不到任何数据，本身就有问题，不应该短拣，不显示
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //判断所属产品编码是否有属于尾箱且实发数量小于应发数量的记录
                                    var item = list.FirstOrDefault(o => o.MATNR == matnr && o.ISLAST == "是" && o.Add_RQTY < o.QTY);
                                    if (item != null)
                                    {
                                        item.Add_RQTY++;
                                        if (string.IsNullOrEmpty(item.ZCOLSN))
                                        {
                                            item.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                            item.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                            item.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                        }
                                    }
                                    else
                                    {
                                        //判断所属产品编码是否有不是尾箱且实发数量小于应发数量的记录
                                        var tItem = list.FirstOrDefault(o => o.MATNR == matnr && o.ISLAST == "否" && o.Add_RQTY < o.QTY);
                                        if (tItem != null)
                                        {
                                            tItem.Add_RQTY++;
                                            if (string.IsNullOrEmpty(tItem.ZCOLSN))
                                            {
                                                tItem.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                                tItem.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                                tItem.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                            }
                                        }
                                        else
                                        {
                                            //获取所属产品编码最后一条记录，看是否存在
                                            var lastItem = list.LastOrDefault(o => o.MATNR == matnr);
                                            if (lastItem != null)
                                            {
                                                lastItem.Add_RQTY++;
                                                if (string.IsNullOrEmpty(lastItem.ZCOLSN))
                                                {
                                                    lastItem.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                                    lastItem.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                                    lastItem.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                                }
                                            }
                                            else
                                            {
                                                //找不到任何数据，本身就有问题，不应该短拣，不显示
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //判断主副条码是否一致
                        if (list.Count(o => o.Add_RQTY > 0 && o.RQTY != o.Add_RQTY) > 0)
                        {
                            HideLoading();
                            Invoke(new Action(() =>
                            {
                                MetroMessageBox.Show(this, "主辅条码不一致，不允许短拣", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }));

                            return;
                        }
                        //end edit

                        int faCount = 0;
                        int saoCount = 0;

                        foreach (ShortPickBoxInfo item in list)
                        {
                            //实发数量和短拣数量都不为0的不显示
                            if (item.RQTY == 0 && item.SHORTQTY == 0)
                                continue;

                            faCount += item.QTY;
                            saoCount += item.RQTY;

                            AddShortPickBoxGrid(item);
                        }

                        Invoke(new Action(() =>
                        {
                            faTotalBoxNum.Text = faCount.ToString();
                            saoTotalNum.Text = saoCount.ToString();
                        }));

                    }
                    else
                    {
                        HideLoading();
                        Invoke(new Action(() =>
                        {
                            MetroMessageBox.Show(this, "该箱没有短拣信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }));
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                }
                HideLoading();
            }));
            thread.IsBackground = true;
            thread.Start();
        }

        private void AddShortPickBoxGrid(ShortPickBoxInfo item)
        {
            Invoke(new Action(() =>
            {
                if (item.SHORTQTY > 0)
                {
                    gridShort.Rows.Insert(0, item.HU, item.PICK_TASK, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.QTY, item.RQTY, item.SHORTQTY, item.ISLAST);
                    gridShort.Rows[0].Tag = item;

                    gridShort.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;

                }
                else
                {
                    gridShort.Rows.Add(item.HU, item.PICK_TASK, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.QTY, item.RQTY, item.SHORTQTY, item.ISLAST);
                    gridShort.Rows[gridShort.Rows.Count - 1].Tag = item;

                }

            }));
        }

        private void btnShortConfirm_Click(object sender, EventArgs e)
        {
            if (gridShort.Rows.Count <= 0) return;
            //登录验证
            ShortConfirmForm form = new ShortConfirmForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                List<PKDeliverBoxShortPickDetailInfo> shortList = new List<PKDeliverBoxShortPickDetailInfo>();
                foreach (DataGridViewRow row in gridShort.Rows)
                {
                    PKDeliverBoxShortPickDetailInfo item = new PKDeliverBoxShortPickDetailInfo();
                    item.HU = (row.Tag as ShortPickBoxInfo).HU;
                    item.LGNUM = SysConfig.LGNUM;
                    item.PICK_TASK = (row.Tag as ShortPickBoxInfo).PICK_TASK;
                    item.PICK_TASK_ITEM = (row.Tag as ShortPickBoxInfo).PICK_TASK_ITEM;
                    item.MATNR = (row.Tag as ShortPickBoxInfo).MATNR;
                    item.QTY = (row.Tag as ShortPickBoxInfo).RQTY;
                    item.DJQTY = (row.Tag as ShortPickBoxInfo).SHORTQTY;
                    if (item.QTY == 0 && item.DJQTY == 0)
                        continue;

                    shortList.Add(item);
                }
                if (LocalDataService.SaveShortPickDetail(shortList))
                {
                    MetroMessageBox.Show(this, "短拣成功，请重新投放通道机检测", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //清空列表
                    this.Invoke(new Action(() =>
                    {
                        gridShort.Rows.Clear();
                    }));
                }
                else
                {
                    MetroMessageBox.Show(this, "短拣失败，可能是网络不稳定，请稍候重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnQueryDeliverBox_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeliverHu.Text.Trim()))
            {
                MetroMessageBox.Show(this, "箱码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            gridDeliverErrorBox.Rows.Clear();
            List<PKDeliverErrorBox> box = PKDeliverService.GetDeliverErrorBoxByHu(txtDeliverHu.Text.Trim());
            if (box?.Count > 0)
            {
                foreach (PKDeliverErrorBox item in box)
                {
                    gridDeliverErrorBox.Rows.Add(item.PARTNER, item.HU, item.PICK_TASK, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.REAL, item.DIFF, item.REMARK);
                }
            }
            else
            {
                MetroMessageBox.Show(this, "未找到本箱信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.lblCount.Text = "0";
            this.grid.Rows.Clear();
            this.epcList.Clear();
            if (SysConfig.IsTest)
            {
                reportEpc("50002AE00508C0000000123456");
            }

        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            this.lblCount.Text = "0";
            metroGrid1.Rows.Clear();
            this.epcList.Clear();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.lblCount.Text = "0";
            metroGrid2.Rows.Clear();
            this.epcList.Clear();
        }


    }
}
