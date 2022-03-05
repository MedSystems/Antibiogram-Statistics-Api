using MedicalSystems.Antibiogram.StatisticsApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MedicalSystems.Antibiogram.StatisticsApi.Services;

public class LabsService
{
	private readonly IMongoCollection<Lab> _collection;
	
	public LabsService(IOptions<MongoDbSettings> databaseSettings)
	{
		var client = new MongoClient(databaseSettings.Value.ConnectionString);
		var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
		_collection = database.GetCollection<Lab>(databaseSettings.Value.LabsCollectionName);
	}
	
	public async Task<List<Lab>> GetAsync() => await _collection.Find(item => true).ToListAsync();

	public async Task<Lab?> GetAsync(string id) => await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();
	
	public async Task CreateAsync(Lab newItem) => await _collection.InsertOneAsync(newItem);

	public async Task UpdateAsync(string id, Lab updatedItem) => await _collection.ReplaceOneAsync(Item => Item.Id == id, updatedItem);
	
	public async Task RemoveAsync(string? id) => await _collection.DeleteOneAsync(item => item.Id == id);
}
