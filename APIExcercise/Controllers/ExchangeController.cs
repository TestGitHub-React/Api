using APIExcercise.Classes;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIExcercise.Controllers
{
    [AllowAnonymous]
    public class ExchangeController : ApiController
    {
        private IExchangeService _exchangeService;

        
        public ExchangeController(IExchangeService exchangeService)
        {
            this._exchangeService = exchangeService;
        }

        [HttpPost]
        [ActionName("ExchangesByDates")]
        public HttpResponseMessage GetExchangesByDates([FromBody]ExchangeHistoryQueryData exchangeHistoryQueryData)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _exchangeService.GetExchangeHistoryForDates(exchangeHistoryQueryData));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
