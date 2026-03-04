using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebCoreProject.Models;
using Microsoft.IdentityModel.Tokens;

namespace WebCoreProject.ViewComponents.Exchange
{
	public class ExchangeRate : ViewComponent
	{

		private readonly IMemoryCache _memoryCache;

		public ExchangeRate(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{

			const string cacheKey = "doviz_altin_verileri";

		

			// 3. ÖNCE HAFIZAYA BAK: Eğer "doviz_altin_verileri" hafızada VARSA, hiç API'ye gitme, direkt onu kullan
			if (!_memoryCache.TryGetValue(cacheKey, out ExchangeRatesViewModel model) )
			{

				model = new ExchangeRatesViewModel();

				using (var client = new HttpClient())
				{

					try
					{

						string dovizUrl = "https://api.genelpara.com/json/?list=doviz&sembol=USD,EUR";
						string altinUrl = "https://api.genelpara.com/json/?list=altin&sembol=GA";

						// 1. İki adresten de verileri asenkron olarak çekiyoruz
						var responseDoviz = await client.GetStringAsync(dovizUrl);
						var responseAltin = await client.GetStringAsync(altinUrl);

						// 2. Gelen metinleri geçici modellere dönüştürüyoruz
						var dovizVerileri = JsonSerializer.Deserialize<GenelParaResponse>(responseDoviz);
						var altinVerileri = JsonSerializer.Deserialize<GenelParaResponse>(responseAltin);

						if (dovizVerileri?.Data != null && altinVerileri?.Data != null)
						{
							model.USD = dovizVerileri.Data.USD;
							model.EUR = dovizVerileri.Data.EUR;

							model.GA = altinVerileri.Data.GA;
						}


						var cacheOptions = new MemoryCacheEntryOptions()
						.SetAbsoluteExpiration(TimeSpan.FromMinutes(15)); // Süreyi buradan değiştirebilirsin

						_memoryCache.Set(cacheKey, model, cacheOptions);


					}
					catch
					{
						// İnternet koparsa veya API yanıt vermezse site patlamaz, boş kartlar döner
					}

				}	

			}

			return View(model);



		}

	}
}
