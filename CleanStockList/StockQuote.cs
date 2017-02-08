using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanStockList
{
    class StockQuote
    {

        public string Status
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Symbol
        {
            get;
            set;
        }
        public double LastPrice
        {
            get;
            set;
        }

        public double Change
        {
            get;
            set;
        }

        public double ChangePercent
        {
            get;
            set;
        }

        public string Timestamp
        {
            get;
            set;
        }

        public double MSDate
        {
            get;
            set;
        }

        public double MarketCap
        {
            get;
            set;
        }

        public Int32 Volume
        {
            get;
            set;
        }

        public double ChangeYTD
        {
            get;
            set;
        }

        public double ChangePercentYTD
        {
            get;
            set;
        }

        public double High
        {
            get;
            set;
        }

        public double Low
        {
            get;
            set;
        }

        public double Open
        {
            get;
            set;
        }
    }
}
