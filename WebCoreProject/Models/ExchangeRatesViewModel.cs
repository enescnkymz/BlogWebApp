using System.Text.Json.Serialization;

namespace WebCoreProject.Models
{
	public class CurrencyDetail
	{
		
		[JsonPropertyName("satis")]
		public string Price { get; set; }

		[JsonPropertyName("degisim")]
		public string Change { get; set; }

	}


	public class ExchangeRatesViewModel
	{

		public CurrencyDetail USD { get; set; }
		public CurrencyDetail EUR { get; set; }
		public CurrencyDetail GA { get; set; }

	}

	public class GenelParaResponse
	{
		[JsonPropertyName("data")]
		public ExchangeRatesViewModel Data { get; set; }

	}

}