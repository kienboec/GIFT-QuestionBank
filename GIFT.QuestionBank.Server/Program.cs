using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using GIFT.QuestionBank.Shared.Model;

namespace GIFT.QuestionBank.Server
{
    class Program
    {
        private static QuestionBankManager _questionBankManager = new QuestionBankManager();
        private static object locker = new object();

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 8000);
            listener.Start(5);

            while (true)
            {
                var tcpClient = listener.AcceptTcpClient();
                Task.Run(() =>
                {
                    ClientConnectionHandling(tcpClient);
                });
            }
        }

        public static void ClientConnectionHandling(TcpClient client)
        {
            using var reader = new StreamReader(client.GetStream());
            using var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

            string command = null;
            while (command != "QUIT")
            {
                command = reader.ReadLine()?.Trim()?.ToUpper() ?? "QUIT";
                if (command == "LIST")
                {
                    List<Question> questions;
                    lock (locker)
                    {
                        questions = _questionBankManager.GetQuestions();
                    }

                    foreach (var question in questions)
                    {
                        writer.WriteLine(question.QuestionName);
                    }

                    writer.WriteLine();
                }
                else if (command == "GET")
                {
                    var questionName = reader.ReadLine();
                    Question question;
                    lock (locker)
                    {
                        question = _questionBankManager
                            .GetQuestions()
                            .FirstOrDefault(x => x.QuestionName == questionName);
                    }

                    writer.WriteLine(question.ToGIFTString());
                    writer.WriteLine();
                }
            }

            client.Close();
        }
    }
}
