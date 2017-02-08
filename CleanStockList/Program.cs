using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RestSharp;
using System.Threading;

namespace CleanStockList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world from CleanStockList");

            Console.WriteLine("Start cleaning NYSE");
            ProcessFile("StocksNYSE.txt");

            Console.WriteLine("Start cleaning NASDAQ");
            ProcessFile("StocksNASDAQ.txt");

            Console.WriteLine("All done");
            Console.ReadLine();
        }

        static void ProcessFile(string filePath)
        {
            //Read the list of code into memory
            var symbols = File.ReadLines(filePath).Select(line =>
            {
                var elements = line.Split('\t');
                return elements[1];
            }).Distinct().ToList();

            //Prepare Restsharp
            var client = new RestClient("http://dev.markitondemand.com/");
            //var request = new RestRequest("/MODApis/Api/v2/Quote?symbol={code}", Method.GET);

            //Write into a target file
            var targetFileName = Path.GetFileNameWithoutExtension(filePath) + "_Valid.txt";
            using (var file = File.CreateText(targetFileName))
            {
                foreach (var symbol in symbols)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(2)); //so as not go over the limit of MarkitOnDemand

                    //request.AddUrlSegment("code", symbol);
                    var request = new RestRequest($"/MODApis/Api/v2/Quote?symbol={symbol}", Method.GET);
                    var response = client.Execute<StockQuote>(request);
                    var stock = response.Data;

                    if (stock != null && !string.IsNullOrEmpty(stock.Name))
                    {
                        file.WriteLine(String.Format($"{stock.Symbol}\t{stock.Name}"));
                    }
                }
            }
        }
    }
}
