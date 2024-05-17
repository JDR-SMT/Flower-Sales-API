using FlowerSales.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FlowerSales.API.Services
{
	public class FlowersService
	{
		private readonly IMongoCollection<Flower> _flowersCollection;

		public FlowersService(
			IOptions<FlowerSalesDatabaseSettings> flowerSalesDatabaseSettings)
		{
			var mongoClient = new MongoClient(
				flowerSalesDatabaseSettings.Value.ConnectionString);

			var mongoDatabase = mongoClient.GetDatabase(
				flowerSalesDatabaseSettings.Value.DatabaseName);

			_flowersCollection = mongoDatabase.GetCollection<Flower>(
				flowerSalesDatabaseSettings.Value.FlowersCollectionName);
		}

		// list all flowers
		public async Task<List<Flower>> GetAsync() =>
			await _flowersCollection.Find(f => true).ToListAsync();

		// list available flowers
		public async Task<List<Flower>> GetAvailableAsync() =>
			await _flowersCollection.Find(f => f.IsAvailable).ToListAsync();

		// return flower by id
		public async Task<Flower?> GetAsync(string id) =>
			await _flowersCollection.Find(f => f.Id == id).FirstOrDefaultAsync();

		// add a new flower
		public async Task AddAsync(Flower newFlower) =>
			await _flowersCollection.InsertOneAsync(newFlower);

		// update an existing flower by id
		public async Task UpdateAsync(string id, Flower updatedFlower) =>
			await _flowersCollection.ReplaceOneAsync(f => f.Id == id, updatedFlower);

		// delete an existing flower by id
		public async Task DeleteAsync(string id) =>
			await _flowersCollection.DeleteOneAsync(f => f.Id == id);
	}
}
