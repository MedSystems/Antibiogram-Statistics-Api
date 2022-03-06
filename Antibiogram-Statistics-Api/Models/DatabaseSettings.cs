namespace MedicalSystems.Antibiogram.StatisticsApi.Models;

public class MongoDbSettings
{
	public string? PatientsCollectionName { get; set; } = null;
	public string? LabsCollectionName { get; set; } = null;
	public string? ConnectionString { get; set; } = null;
	public string? DatabaseName { get; set; } = null;
}
