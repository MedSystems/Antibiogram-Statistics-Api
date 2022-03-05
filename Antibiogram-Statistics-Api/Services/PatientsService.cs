using MedicalSystems.Antibiogram.StatisticsApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MedicalSystems.Antibiogram.StatisticsApi.Services;

public class PatientsService
{
	private readonly IMongoCollection<Patient> _collection;
	
	public PatientsService(IOptions<MongoDbSettings> databaseSettings)
	{
		var client = new MongoClient(databaseSettings.Value.ConnectionString);
		var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
		_collection = database.GetCollection<Patient>(databaseSettings.Value.PatientsCollectionName);
	}
	
	public async Task<List<Patient>> GetAsync() => await _collection.Find(item => true).ToListAsync();

	public async Task<Patient?> GetAsync(string id) => await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();
	
	public async Task CreateAsync(Patient newItem) => await _collection.InsertOneAsync(newItem);

	public async Task UpdateAsync(string id, Patient updatedItem) => await _collection.ReplaceOneAsync(item => item.Id == id, updatedItem);
	
	public async Task RemoveAsync(string? id) => await _collection.DeleteOneAsync(item => item.Id == id);
}
