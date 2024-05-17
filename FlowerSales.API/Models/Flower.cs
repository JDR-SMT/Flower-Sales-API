using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FlowerSales.API.Models
{
	public class Flower
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; internal set; }

		[Required]
		public string Category { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string StoreLocation { get; set; }

		[Required]
		public int PostCode { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		public bool IsAvailable { get; set; }
	}
}
