using DateAccessLayer.Migrations;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DateAccessLayer.Concrete
{
	public class Context : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("workstation id=BlogCoreDb.mssql.somee.com;packet size=4096;user id=eagrez_SQLLogin_2;pwd=4er1yr6bdo;data source=BlogCoreDb.mssql.somee.com;persist security info=False;initial catalog=BlogCoreDb;TrustServerCertificate=True");

		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Descriptions tablosu için HasTrigger sınıfını ekleyin
			modelBuilder.Entity<Description>()
				.ToTable("Descriptions", t => t.HasTrigger("ANY_TRIGGER_NAME")); // Tetikleyici adı önemli değil, varlığını belirtmek yeterli

			modelBuilder.Entity<Comment>()
				.ToTable("Comments", t => t.HasTrigger("ANY_TRIGGER_NAME")); // Tetikleyici adı önemli değil, varlığını belirtmek yeterli

			// 1. GÖNDEREN İlişkisi (Sender Relationship)
			modelBuilder.Entity<Message>()
				.HasOne(m => m.Sender)        // Message'ın bir adet Sender'ı var
				.WithMany(u => u.SenderMessages) // User'ın birden çok SentMessages'ı var
				.HasForeignKey(m => m.SenderID) // Yabancı anahtar Message.SenderId
				.OnDelete(DeleteBehavior.ClientSetNull); // Opsiyonel: Cascade delete'i engeller

			// 2. ALICI İlişkisi (Receiver Relationship)
			modelBuilder.Entity<Message>()
				.HasOne(m => m.Receiver)        // Message'ın bir adet Receiver'ı var
				.WithMany(u => u.ReceiverMessages) // User'ın birden çok ReceivedMessages'ı var
				.HasForeignKey(m => m.ReceiverID) // Yabancı anahtar Message.ReceiverId
				.OnDelete(DeleteBehavior.ClientSetNull); // Opsiyonel: Cascade delete'i engeller


			// Description - Writer 
			modelBuilder.Entity<Description>()
				.HasOne(d => d.Writer)
				.WithMany(w => w.Descriptions)
				.HasForeignKey(d => d.WriterID)
				.OnDelete(DeleteBehavior.Cascade); 

			// Comment - Writer 
			modelBuilder.Entity<Comment>()
				.HasOne(c => c.CommentSender)
				.WithMany(w => w.Comments)
				.HasForeignKey(c => c.CommentSenderID)
				.OnDelete(DeleteBehavior.Cascade); 

			// Comment - Description 
			modelBuilder.Entity<Comment>()
				.HasOne(c => c.Description)
				.WithMany(d => d.Comments)
				.HasForeignKey(c => c.DescriptionID)
				.OnDelete(DeleteBehavior.NoAction);

			// **ÖNEMLİ NOT: Birden fazla ilişki aynı tabloya bağlandığı için,
			// SQL Server'ın döngüsel silme kısıtlaması hatasını almamak için 
			// iki ilişkiden en az birine veya her ikisine
			// `.OnDelete(DeleteBehavior.Restrict)` eklenmesi şiddetle tavsiye edilir.**

			base.OnModelCreating(modelBuilder);
		}



		public DbSet<About> Abouts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Writer> Writers { get; set; }
		public DbSet<Description> Descriptions { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<NewsLetter> NewsLetters { get; set; }
		public DbSet<BlogRating> BlogRatings { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Message> Messages { get; set; }



	}
}