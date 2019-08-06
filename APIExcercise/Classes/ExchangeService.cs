using APIExcercise.Properties;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;

namespace APIExcercise.Classes
{
    public class ExchangeService : IExchangeService
    {
        public object GetExchangeHistoryForDates(ExchangeHistoryQueryData exchangeHistoryQueryData)
        {
            StringBuilder symbols = new StringBuilder();
            symbols.AppendFormat(Resources.ExchangeHistorySymbolsFormat, exchangeHistoryQueryData.CurrencyFrom, exchangeHistoryQueryData.CurrencyTo);

            string dateFrom = exchangeHistoryQueryData.Dates.Min();
            string dateTo = exchangeHistoryQueryData.Dates.Max();

            StringBuilder urlParameters = new StringBuilder();
            urlParameters.AppendFormat(Resources.ExchangeHistoryUrlParamsTemplate, dateFrom, dateTo, symbols.ToString());

            APICallBuilder apiCallGet = new APICallGet();
            apiCallGet.
                SetBaseAddress(Resources.ExchangeHistoryBaseAddress).
                SetURLParameters(urlParameters.ToString()).
                SetAcceptMediaType(MediaType.JSON);

            var jObj = apiCallGet.PlaceAPICall();

            var result = jObj[Resources.ExchangeHistoryNode].ToList().
                Select(el => new
                {
                    date = ((JProperty)el).Name,
                    value = el.Children().First().Value<double>(exchangeHistoryQueryData.CurrencyTo) /
                            el.Children().First().Value<double>(exchangeHistoryQueryData.CurrencyFrom)
                }).
                Where(d => exchangeHistoryQueryData.Dates.Contains(d.date)).
                OrderBy(v => v.value);

            var finalResult = new
            {
                MinRate = result.First().value,
                MinDate = result.First().date,
                MaxRate = result.Last().value,
                MaxDate = result.Last().date,
                AvgRate = result.Average(a => a.value)
            };

            return finalResult;
        }
    }

    public interface IExchangeService
    {
        object GetExchangeHistoryForDates(ExchangeHistoryQueryData exchangeHistoryQueryData);
    }

}