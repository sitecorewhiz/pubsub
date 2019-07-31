using MessagePayLoad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    public class PublisherEntryPoint
    {
        public static void Main(string[] args)
        {
            MessageQueue messageQueue = null;

            string path = @".\Private$\pubsub";

            Console.WriteLine("Enter a message to transmit.... Press x to exit");
            string input = Console.ReadLine();

            while (input != "x")
            {
                PayLoad payLoad = new PayLoad { MessageBody = input };

                try
                {
                    Message msg = new Message();
                    msg.Label = "Payload from Publisher";


                    msg.Body = payLoad;


                    if (!MessageQueue.Exists(path))
                    {
                        //No Q exists, hence create it
                        messageQueue = MessageQueue.Create(path);
                    }
                    else
                    {
                        messageQueue = new MessageQueue(path);
                    }
                    messageQueue.Send(payLoad);

                    Console.WriteLine($"Message {input} transmitted to the Queue");
                    Console.WriteLine("Enter a message to transmit.... Press x to exit");
                    input = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }
    }
}
