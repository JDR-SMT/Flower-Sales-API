namespace FlowerSales.API.Models
{
	public class FlowerQueryParameters : QueryParameters
	{
		public string? Category { get; set; }

		public string? Name { get; set; }

		public string? StoreLocation { get; set; }

		public int? PostCode { get; set; }

		public decimal? MinPrice { get; set; }

		public decimal? MaxPrice { get; set; }
	}
}
