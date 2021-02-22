using BLZService;
using System;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter BLZ code to get bank details. Sample BLZ codes can be found here http://finaint.com/BLZ-List/200.html");

            while (true)
            {
                try
                {
                    String blzCode = Console.ReadLine();
                    BLZServicePortTypeClient service = new BLZServicePortTypeClient(BLZServicePortTypeClient.EndpointConfiguration.BLZServiceSOAP12port_http);
                    getBankResponse response = service.getBankAsync(new getBankRequest(blzCode)).Result;

                    if (response != null)
                    {

                        Console.WriteLine("Bank details:");
                        Console.WriteLine(String.Format("Bank Name: {0}", response.details.bezeichnung));
                        Console.WriteLine(String.Format("Swift Code: {0}", response.details.bic));
                        Console.WriteLine(String.Format("Address: {0}", response.details.ort));
                        Console.WriteLine(String.Format("ORT: {0}", response.details.plz));
                        Console.ReadLine();
                        break;
                    } else
                    {
                        Console.WriteLine("No data found for this BLZ code, please try again");
                        Console.WriteLine("-------------------------------------------------------------------");
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("One or more errors occurred"))
                    {
                        Console.WriteLine("Invalid BLZ code, please try again");
                        Console.WriteLine("-------------------------------------------------------------------");
                        continue;
                    }
                    Console.WriteLine("An error occured:");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                }
            }
        }
    }
}
