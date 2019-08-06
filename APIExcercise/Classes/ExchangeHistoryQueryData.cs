using System.Collections.Generic;

namespace APIExcercise.Classes
{
    public class ExchangeHistoryQueryData
    {
        public List<string> Dates { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
    }
}