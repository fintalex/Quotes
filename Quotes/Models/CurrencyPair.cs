using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
	/// <summary>
	/// Этот класс представляет собой отношение между 2 валютами.
	/// </summary>
	public class CurrencyPair
	{
		private String from;
		private String to;
		public float price;
		public CurrencyPair(String from, String to)
		{
			this.from = from;
			this.to = to;
		}
		public String getFrom()
		{
			return this.from;
		}
		public void setFrom(String from)
		{
			this.from = from;
		}
		public String getTo()
		{
			return this.to;
		}
		public void setTo(String to)
		{
			this.to = to;
		}
		public float getPrice()
		{
			return price;
		}
	}
}