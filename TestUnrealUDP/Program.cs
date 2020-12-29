using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mime;
using TestUnrealUDP;
using System.Windows.Forms;

namespace TestControlPanel
{
    class Program
    {
        private static UdpClient udpClient;
        private static IPEndPoint ipAddress;
        private static bool isConnected;
        private static bool isDispose;

        [STAThread]
        static void Main()
        {
            var host = ConfigurationSettings.AppSettings["host"];
            var port = ConfigurationSettings.AppSettings["port"];

            if (!int.TryParse(port, out var portResult))
            {
                portResult = 5588;
            }

            if (!IPAddress.TryParse(host, out var reIpAddress))
            {
                reIpAddress = IPAddress.Parse("127.0.0.1");
            }

            ipAddress = new IPEndPoint(reIpAddress, portResult);
            udpClient = new UdpClient();
            new Thread(MachineProcess) { Name = $"UDP machine thread" }.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WindowPanel());
        }

//        public static void Main(string[] args)
//        {
//            var form = new WindowPanel();
            


//            var host = ConfigurationSettings.AppSettings["host"];
//            var port = ConfigurationSettings.AppSettings["port"];

//            if (!int.TryParse(port, out var portResult))
//            {
//                portResult = 5588;
//            }

//            if (!IPAddress.TryParse(host, out var reIpAddress))
//            {
//                reIpAddress = IPAddress.Parse("127.0.0.1");
//            }

//            Console.WriteLine($"Host: {reIpAddress.ToString()}");
//            Console.WriteLine($"Port: {portResult}");
//;
//            ipAddress = new IPEndPoint(reIpAddress, portResult);
//            udpClient = new UdpClient();
//            new Thread(MachineProcess) { Name = $"UDP machine thread" }.Start();

//            while (true)
//            {
//                Console.Write("Id: ");
//                string id = Console.ReadLine();
//                if (int.TryParse(id, out var index))
//                {
//                    Console.Write("Name: ");
//                    string name = Console.ReadLine();
//                    if (!string.IsNullOrEmpty(name))
//                    {
//                        Command.AddPlayer(index, name);
//                        while (true)
//                        {
//                            Console.WriteLine(".....................................");
//                            Console.WriteLine("Message sent");
//                            Console.WriteLine("Retry sending ? y/n");
//                            var yesOrNo = Console.ReadLine();
//                            if (yesOrNo == "y" || yesOrNo == "Y")
//                            {
//                                Command.AddCommand(lastCommand);
//                            }
//                            else
//                            {
//                                break;
//                            }
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("Invalid name");
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("Invalid index");
//                }
//            }
//        }

        private static string lastCommand = null;

        private static void MachineProcess()
        {
            while (!isDispose)
            {
                var currentCommand = Command.GetNextCommand();
                if (currentCommand!=null)
                {
                    lastCommand = currentCommand;
                    try
                    {
                        if (!isConnected)
                        {
                            udpClient.Connect(ipAddress);
                            isConnected = true;
                        }
                        byte[] data = Encoding.ASCII.GetBytes(currentCommand);
                        udpClient.Send(data, data.Length);
                    }
                    catch (Exception e)
                    {
                        isConnected = false;
                        udpClient.Close();
                    }
                    Thread.Sleep(50);
                }
            }
        }
    }
}
