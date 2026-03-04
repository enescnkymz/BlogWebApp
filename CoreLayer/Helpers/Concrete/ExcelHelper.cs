using ClosedXML.Excel;
using CoreLayer.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Helpers.Concrete
{
	public class ExcelHelper : IExcelHelper
	{
		public byte[] ExportToExcel<T>(List<T> list, string sheetName) where T : class
		{
			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add(sheetName);

				// --- 1. ADIM: Başlıkları Yazdırma ve Şekillendirme ---

				// T tipindeki nesnenin özelliklerini (Sütun İsimleri) al
				var properties = typeof(T).GetProperties();

				// Başlıkları döngüyle yaz
				for (int i = 0; i < properties.Length; i++)
				{
					worksheet.Cell(1, i+1 ).Value = properties[i].Name;
				}

				// --- STİL AYARLARI BURADA ---				
				var headerRange = worksheet.Range(1, 1, 1, properties.Length);
				headerRange.Style.Fill.BackgroundColor = XLColor.DarkBlue;			
				headerRange.Style.Font.FontColor = XLColor.White;
				headerRange.Style.Font.Bold = true;
				headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

				// --- 2. ADIM: İçerikleri Yazdırma ---
				int row = 2;
				foreach (var item in list)
				{
					for (int i = 0; i < properties.Length; i++)
					{
						var value = properties[i].GetValue(item);
						worksheet.Cell(row, i + 1).Value = value?.ToString();
					}
					row++;
				}

				
				// Sütunları içindeki en uzun yazıya göre ayarlar
				worksheet.Columns().AdjustToContents();

				// Dosyayı byte dizisine çevir ve döndür
				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					return stream.ToArray();
				}
			}
		}
	}
}
