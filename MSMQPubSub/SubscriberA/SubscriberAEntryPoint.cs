using MessagePayLoad;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SubscriberA
{
   public class SubscriberAEntryPoint
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hit S to subscribe to Publisher at any time... Press X to exit");
     
            string input = Console.ReadLine();

            List<PayLoad> allMessages = new List<PayLoad>();

            string path = @".\Private$\pubsub";

            while(input=="s" || input != "x")
            {
                if(input=="u")
                {
                    Console.WriteLine("Hit S to subscribe and view all messages.... Press X to exit");
                    input = Console.ReadLine();
                    if (input == "s")
                        continue;
                    else if (input == "u" || input == "x")
                        break;
                }

                try
                {
                    using (MessageQueue messageQueue = new MessageQueue(path))
                    {
                        Message[] messages = messageQueue.GetAllMessages();
                        if(messages.Count()>0)
                        {
                            foreach (Message msg in messages)
                            {
                                msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(PayLoad) });
                                var receivedMessage = (PayLoad)msg.Body;
                                Console.WriteLine($"Message Received from Publisher {msg.Label} is:{receivedMessage.MessageBody}");
                            }
                        }
                    }

                    Console.WriteLine("Hit U to subscribe or unsubscribe to Publisher at any time.... Press x to exit");
                    Console.WriteLine("Press any other key to retrieve messages from Publisher");
                    input = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
