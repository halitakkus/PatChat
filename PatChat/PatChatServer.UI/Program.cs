using PatChat.Entities.Entities;
using PatChat.UIObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatChatServer.UI
{
    class Program
    {
        public static void line()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write("-");
            }
            Console.Write("\n");
        }
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 1234);
            listener.Start();
            line();
            Console.WriteLine("START SERVER ..");

            Socket ClientSocket = listener.AcceptSocket();

            if (!ClientSocket.Connected)
            {
                Console.WriteLine("Connected failed");
            }
            else
            {
                string ipaddress = ClientSocket.RemoteEndPoint.ToString().Split(':')[0];
                Console.WriteLine("Connect-> " + ipaddress);
                line();

                while (true)
                {
                    NetworkStream networkStream = new NetworkStream(ClientSocket);
                    StreamWriter networkwriter = new StreamWriter(networkStream);
                    StreamReader Reading = new StreamReader(networkStream);
                    try
                    {
                        string IstemciString = Reading.ReadLine();
                        Message mess = Serialize.JsonDeserialize<Message>(IstemciString);
                        Console.WriteLine("Request:" + ipaddress+" / "+ mess.Content);
                        string gidenMesaj = "OK:"+ ipaddress;
                        Console.WriteLine("Response: " + gidenMesaj);
                        line();
                        networkwriter.WriteLine(gidenMesaj);
                        networkwriter.Flush();
                    }
                    catch{return;}
                }
            }
            ClientSocket.Close();
        }
    }
}
