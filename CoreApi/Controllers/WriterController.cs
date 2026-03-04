using BusinessLayer.Abstract;
using CoreApi.DTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WriterController : ControllerBase
	{
		private readonly IWriterService _writerService;

		public WriterController(IWriterService writerService)
		{
			_writerService = writerService;
		}

		// 2. Listeleme İsteği (GET)
		[HttpGet] // Tarayıcıdan veya Postman'den yapılacak istek türü
		public IActionResult WriterList()
		{
			// Business katmanındaki metodu çağırıyoruz
			var values = _writerService.GetAll();

			var writers = values.Select(x => new WriterApiDto
			{
				Id = x.WriterID,
				Name = x.WriterName,
				Status = x.WriterStatus,}
			);

			// API dünyasında "View" döndürmeyiz, "Durum Kodu ve Veri" döndürürüz.
			// Ok() -> 200 Başarılı kodu ve veriyi (JSON) döner.
			return Ok(writers);
		}

		// Örnek: ID'ye göre getirme
		[HttpGet("{id}")]
		public IActionResult GetWriterById(int id)
		{
			var writer = _writerService.GetById(id);
			if (writer == null)
			{
				return NotFound(); // 404 Bulunamadı hatası döner
			}
			var writerDto = new WriterApiDto 
			{ 
				Id = writer.WriterID,
				Name = writer.WriterName, 
				Status = writer.WriterStatus,
				
			};
			
			return Ok(writerDto);
		}

		[HttpPost]
		public IActionResult AddWriter(WriterApiDto model) 
		{

			Writer w = new Writer();

			w.WriterName = model.Name;
			w.WriterStatus = model.Status;						
			w.WriterImage = "default.png"; 
			_writerService.TAdd(w);
			return Ok("Yazar başarıyla eklendi!");

		}

		[HttpDelete("{id}")]
		public IActionResult DeleteWriter(int id)
		{
			var writerToDelete = _writerService.GetById(id);
			if (writerToDelete == null) 
			{
				return NotFound("Böyle bir yazar bulunamadı!");
			}
			_writerService.TDelete(writerToDelete);
			return Ok("Yazar başarıyla silindi!");
		}

		[HttpPut]
		public IActionResult UpdateWriter(WriterApiDto model)
		{
			if(model.Id == 0)
			{
				return BadRequest("Lütfen ID bilgisini gönderin!");
			}

			var existingWriter = _writerService.GetById(model.Id);

			if (existingWriter == null)
			{
				return NotFound("Güncellenecek yazar bulunamadı.");
			}

			existingWriter.WriterName = model.Name;
			existingWriter.WriterStatus = model.Status;
			

			_writerService.TUpdate(existingWriter);
			return Ok("Güncelleme işlemi başarılı.");


		}



	}
}
