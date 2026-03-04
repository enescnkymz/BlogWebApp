using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using WebCoreProject.Models;

namespace WebCoreProject.ViewComponents.Weather
{
	public class WeatherDashboard : ViewComponent
	{
		private readonly IMemoryCache _memoryCache;

		public WeatherDashboard(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{

			string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

			string cacheKey = $"weather_{ipAddress}";

			if (!_memoryCache.TryGetValue(cacheKey, out WeatherViewModel model))
			{

				// Varsayılan Koordinatlar (Eğer IP bulunamazsa veya Localhost'taysak)
				string lat = "41.0082"; // İstanbul
				string lon = "28.9784";
				string city = "İstanbul";

				using (var client = new HttpClient())
				{
					try
					{
						// 2. IP'den Konum Bulma (Localhost değilse çalışır)
						if (!string.IsNullOrEmpty(ipAddress) && ipAddress != "::1" && ipAddress != "127.0.0.1")
						{

							string ipApiUrl = $"http://ip-api.com/json/{ipAddress}";
							var ipResponse = await client.GetStringAsync(ipApiUrl);
							var ipData = JObject.Parse(ipResponse);

							// Eğer servisten başarıyla konum geldiyse, varsayılan değerleri eziyoruz
							if (ipData["status"]?.ToString() == "success")
							{
								lat = ipData["lat"].ToString();
								lon = ipData["lon"].ToString();
								city = ipData["city"].ToString();
							}
						}

						// 3. Bulunan dinamik koordinatlarla Hava Durumunu Çek
						string apiKey = "36dbdf3d3294a790e0600cd300cf37e2";
						string weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric&mode=json&lang=tr";

						var weatherResponse = await client.GetStringAsync(weatherUrl);
						var document = JObject.Parse(weatherResponse);

						model = new WeatherViewModel();
						model.Temp = document["main"]["temp"].ToString();
						model.Status = document["weather"][0]["description"].ToString();
						model.Icon = document["weather"][0]["icon"].ToString();

						// Modele bulduğumuz şehri de veriyoruz
						model.City = city;
						model.Status = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Status);

						var cacheOptions = new MemoryCacheEntryOptions()
						.SetAbsoluteExpiration(TimeSpan.FromMinutes(15)); 

						_memoryCache.Set(cacheKey, model, cacheOptions);


					}
					catch
					{
						// İnternet kopması vs. durumunda site çökmesin diye boş model
						return View(new WeatherViewModel { Temp = "0", Status = "Bulunamadı", Icon = "01d", City = "Bilinmiyor" });
					}
				}


			}

			return View(model);



		}
	}
}
