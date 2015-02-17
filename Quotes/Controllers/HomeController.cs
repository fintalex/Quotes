using Quotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quotes.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
			YahooCurrencyConverter ycc = new YahooCurrencyConverter();
			try
			{
				String[] currencies = new String[] {"USD", "EUR", "RUB" };
				CurrencyPair[,] currencyPairs = ycc.getConversionMatrix(currencies);
				
			}
			catch (Exception)
			{
				
				throw;
			}
            return View();
        }


		// страница для отображения курсов валют

		public ActionResult Xchanges()
		{

			return View();
		}
		
    }
}
