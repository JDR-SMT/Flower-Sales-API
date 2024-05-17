namespace FlowerSales.API.Models
{
	public class FlowerSalesDatabaseSettings
	{
		public string ConnectionString { get; set; } = null!;

		public string DatabaseName { get; set; } = null!;

		public string FlowersCollectionName { get; set; } = null!;
	}
}
