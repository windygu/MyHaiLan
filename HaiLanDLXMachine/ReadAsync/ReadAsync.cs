using System;
using System.Collections.Generic;
using System.Text;
// for Thread.Sleep
using System.Threading;

// Reference the API
using ThingMagic;

namespace ReadAsync
{
    /// <summary>
    /// Sample program that reads tags in the background and prints the
    /// tags found.
    /// </summary>
    class Program
    {
        static void Usage()
        {
            Console.WriteLine(String.Join("\r\n", new string[] {
                    "Usage: "+"Please provide valid arguments, such as:",
                    "tmr:///com4 or tmr:///com4 --ant 1,2",
                    "tmr://my-reader.example.com or tmr://my-reader.example.com --ant 1,2"
            }));
            Environment.Exit(1);
        }
        static void Main(string[] args)
        {
            // Program setup
            int[] antennaList = {1,2,3,4}; //ѡ������1,2,3,4

            try
            {
                Reader.SetSerialTransport("tcp", SerialTransportTCP.CreateSerialReader);

                // Create Reader object, connecting to physical device.
                // Wrap reader in a "using" block to get automatic
                // reader shutdown (using IDisposable interface).
                using (Reader r = Reader.Create("tcp://192.168.8.166:8086"))  //��������
                {
                    r.Connect();  //���Ӷ�д��

                    r.ParamSet("/reader/region/id", Reader.Region.NA);

                    // Create a simplereadplan which uses the antenna list created above
                    SimpleReadPlan plan = new SimpleReadPlan(antennaList, TagProtocol.GEN2,null,null,1000); //�������ߺ�Э��
                    // Set the created readplan
                    r.ParamSet("/reader/read/plan", plan);

                    //��������,���������
                    Gen2.LinkFrequency blf = Gen2.LinkFrequency.LINK320KHZ;
                    r.ParamSet("/reader/gen2/BLF", blf);

                    Gen2.Tari tari = Gen2.Tari.TARI_6_25US;
                    r.ParamSet("/reader/gen2/tari", tari);

                    Gen2.TagEncoding tagncoding = Gen2.TagEncoding.FM0;
                    r.ParamSet("/reader/gen2/tagEncoding", tagncoding);

                    Gen2.Session session = Gen2.Session.S1;
                    r.ParamSet("/reader/gen2/session", session);

                    Gen2.Target target = Gen2.Target.A;
                    r.ParamSet("/reader/gen2/target", target);

                    //500~3150
                    r.ParamSet("/reader/radio/readPower", 3000);

                    // ע���ǩ���ݻص�����
                    r.TagRead += delegate(Object sender, TagReadDataEventArgs e)
                    {
                        Console.WriteLine("Background read: " + e.TagReadData);
                    };
                    //�����쳣�ص�����
                    r.ReadException += new EventHandler<ReaderExceptionEventArgs>(r_ReadException);

                    // ��ʼ����
                    r.StartReading();

                    Console.WriteLine("\r\n<Do other work here>\r\n");
                    Thread.Sleep(5000);
                    Console.WriteLine("\r\n<Do other work here>\r\n");
                    Thread.Sleep(10000);
                    //��������
                    r.StopReading();
                }
            }
            catch (ReaderException re)
            {
                Console.WriteLine("Error: " + re.Message);
                Console.Out.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void r_ReadException(object sender, ReaderExceptionEventArgs e)
        {
            Console.WriteLine("Error: " + e.ReaderException.Message);
        }

    }
}
