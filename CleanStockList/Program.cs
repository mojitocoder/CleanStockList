using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RestSharp;

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
            }).Distinct().ToList().Take(10);

            //Prepare Restsharp
            var client = new RestClient("http://dev.markitondemand.com/");
            //var request = new RestRequest("/MODApis/Api/v2/Quote?symbol={code}", Method.GET);

            var stocks = new List<StockQuote>();
            foreach (var symbol in symbols)
            {
                //request.AddUrlSegment("code", symbol);
                var request = new RestRequest($"/MODApis/Api/v2/Quote?symbol={symbol}", Method.GET);
                var response = client.Execute<StockQuote>(request);
                var stock = response.Data;

                if (!string.IsNullOrEmpty(stock.Name))
                {
                    stocks.Add(stock);
                }
            }

            //var stocks = symbols.Select(symbol =>
            //{
            //    request.AddUrlSegment("code", symbol);
            //    var response = client.Execute<StockQuote>(request);
            //    return response.Data;
            //}).Where(foo => !string.IsNullOrEmpty(foo.Name)).ToList();

            //Write into a target file
            var targetFileName = Path.GetFileNameWithoutExtension(filePath) + "_Valid.txt";
            File.WriteAllLines(targetFileName, stocks.Select(stock =>
            {
                return String.Format($"{stock.Symbol}\t{stock.Name}");
            }));
        }
    }
}
