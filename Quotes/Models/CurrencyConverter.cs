using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Quotes.Models
{
	public interface CurrencyConverter
	{
		float convert(String currencyFrom, String currencyTo);
		void convert(CurrencyPair[] currencyPairs);
	}

	public abstract class BaseCurrencyConverter : CurrencyConverter
	{
		public CurrencyPair[,] getConversionMatrix(String[] currencies)
		{
			//здесь строим пары для валют
			int size = currencies.Length;
			CurrencyPair[] currencyPairs = new CurrencyPair[size * size];
			int index = 0;
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					currencyPairs[index++] = new CurrencyPair(currencies[i].ToString(), currencies[j].ToString());
				}
			}
			// get currencies information
			convert(currencyPairs);
			// Build matrix
			CurrencyPair[,] matrix = new CurrencyPair[size,size];
			index = 0;
			for (int i = 0; i < size; i++) {
				for (int j = 0; j < size; j++) {
					matrix[i,j] = i != j ? currencyPairs[index] : null;
					index++;
				}
			}
			return matrix;
		}


		public abstract float convert(string currencyFrom, string currencyTo);

		public abstract void convert(CurrencyPair[] currencyPairs);

	}
	public class YahooCurrencyConverter : BaseCurrencyConverter
	{
		public override float convert(String currencyFrom, String currencyTo)
		{
			//HttpClient httpclient = new HttpClient();
			//HttpGet httpget = new HttpGet("http://quote.yahoo.com/d/quotes.csv?s=" + currencyFrom + currencyTo + "=X&f=l1&e=.csv");
			WebRequest request = WebRequest.Create("http://quote.yahoo.com/d/quotes.csv?s=" + currencyFrom + currencyTo + "=X&f=l1&e=.csv");

			return 5;
		}

		public override void convert(CurrencyPair[] currencyPairs)
		{
			HttpClient httpclient = new HttpClient();

			StringBuilder sb = new StringBuilder();
			foreach (CurrencyPair currencyPair in currencyPairs) {
				sb.Append("s=").Append(currencyPair.getFrom()).Append(currencyPair.getTo()).Append("=X&");
			}

			string text = "";
			using (WebClient client = new WebClient())
			{
				text = client.DownloadString("http://quote.yahoo.com/d/quotes.csv?" + sb + "f=l1&e=.csv");
			}
			string parsedText = text.Replace("\r\n", ";").Replace(".", ",");
			
			//WebRequest request = WebRequest.Create("http://quote.yahoo.com/d/quotes.csv?" + sb + "f=l1&e=.csv");

			//ResponseHandler<String> responseHandler = new BasicResponseHandler();
			//String responseBody = httpclient.execute(httpGet, responseHandler);
			//httpclient.getConnectionManager().shutdown();
			String[] lines = parsedText.Substring(0,parsedText.Length-1).Split(';');
			//if (lines.Length != currencyPairs.Length) {
			//	throw new Exception("Currency data mismatch");
			//}
			int i = 0;
			foreach (String line in lines) {
				CurrencyPair currencyPair = currencyPairs[i++];
				currencyPair.price = float.Parse(line);
			}
		}
	}

}