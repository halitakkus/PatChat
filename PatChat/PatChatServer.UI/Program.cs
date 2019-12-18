using PatChat.BusinessLayer;
using PatChat.Entities.Entities;
using PatChat.UIObject;
using System;
using System.Collections;
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
        public static Hashtable clientsList = new Hashtable();
        static void Main(string[] args)

        {
          
            TcpListener serverSocket = new TcpListener(8888);

            TcpClient clientSocket = default(TcpClient);

            int counter = 0;



            serverSocket.Start();

            Console.WriteLine("PATCHAT SUNUCUSU BAŞLADI ....");

            counter = 0;

            while ((true))

            {

                counter += 1;

                clientSocket = serverSocket.AcceptTcpClient();



                byte[] bytesFrom = new byte[10025];

                string dataFromClient = null;



                NetworkStream networkStream = clientSocket.GetStream();

                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);

                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);

                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));


                try
                {
                clientsList.Add(dataFromClient, clientSocket);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hali hazırda böyle bir tcp bağlantısı mevcut");
                    
                }


                broadcast(dataFromClient + " oky$", dataFromClient, false);



                Console.WriteLine(dataFromClient + " Bağlandı ");

                handleClinet client = new handleClinet();

                client.startClient(clientSocket, dataFromClient, clientsList);

            }

            clientSocket.Close();

            serverSocket.Stop();

            Console.WriteLine("exit");

            Console.ReadLine();

        }

        public static void broadcast(string msg, string uName, bool flag)
        {
            GroupBLL GROUP = new GroupBLL();
            foreach (DictionaryEntry Item in clientsList)
            {

                TcpClient broadcastSocket;

                broadcastSocket = (TcpClient)Item.Value;

                NetworkStream broadcastStream = broadcastSocket.GetStream();

                Byte[] broadcastBytes = null;


                if (flag == true)
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(msg);
                }
                else
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(msg);
                }
               
                    broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);

                        broadcastStream.Flush();
                   
              
               
              

            }

        }  //end broadcast function

    }//end Main class
    public class handleClinet
    {
        TcpClient clientSocket;
        string clNo;
        Hashtable clientsList;
        public void startClient(TcpClient inClientSocket, string clineNo, Hashtable cList)
        {
            this.clientSocket = inClientSocket;

            this.clNo = clineNo;

            this.clientsList = cList;

            Thread ctThread = new Thread(doChat);

            ctThread.Start();

        }

        private void doChat()

        {
            int requestCount = 0;

            byte[] bytesFrom = new byte[10025];

            string dataFromClient = null;

            Byte[] sendBytes = null;

            string serverResponse = null;

            string rCount = null;

            requestCount = 0;


            while ((true))
            {
                
                    requestCount = requestCount + 1;

                    NetworkStream networkStream = clientSocket.GetStream();

                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);

                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);

                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                    Console.WriteLine("İstemci - " + clNo + " : " + dataFromClient);

                  //  Message mess = Serialize.JsonDeserialize<Message>(dataFromClient);
                   

                    rCount = Convert.ToString(requestCount);

                    Program.broadcast(dataFromClient+"$", clNo, true);
                    Console.WriteLine("Brodcast başarılı !!");
                

              

            }//end while

        }//end doChat

    }
}
