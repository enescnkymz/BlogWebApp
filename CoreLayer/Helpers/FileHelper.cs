using SixLabors.ImageSharp.Formats.Webp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Webp;
using Microsoft.AspNetCore.Http;

namespace CoreLayer.Helpers
{
    public static class FileHelper
    {

		public static async Task<string> SaveAndCompressImage(IFormFile file, string folderName)
		{
			if (file == null || file.Length == 0) return null;

			// Dosyaya benzersiz bir isim veriyoruz, uzantısını zorla .webp yapıyoruz
			string fileName = Guid.NewGuid().ToString() + ".webp";

			// wwwroot/WriterImages gibi bir yol oluşturuyoruz
			string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);

			// ImageSharp ile dosyayı hafızaya alıyoruz
			using (var image = await Image.LoadAsync(file.OpenReadStream()))
			{
				// 1. BOYUTLANDIRMA: Eğer genişlik 800 pikselden büyükse, orantılı olarak 800'e küçült
				if (image.Width > 800)
				{
					image.Mutate(x => x.Resize(new ResizeOptions
					{
						Size = new Size(800, 0), // 0, yüksekliği orantılı olarak otomatik ayarlar
						Mode = ResizeMode.Max
					}));
				}

				// 2. SIKIŞTIRMA VE KAYDETME: Kaliteyi %75'e çekip WebP olarak kaydediyoruz
				var encoder = new WebpEncoder { Quality = 75 }; // Kaliteyi 1-100 arası ayarlayabilirsin
				await image.SaveAsync(path, encoder);
			}

			return fileName; 

		}

	
	}
}
