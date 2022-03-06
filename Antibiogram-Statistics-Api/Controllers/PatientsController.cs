using MedicalSystems.Antibiogram.StatisticsApi.Models;
using MedicalSystems.Antibiogram.StatisticsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystems.Antibiogram.StatisticsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
	private readonly ILogger<PatientsController> _logger;
	private readonly PatientsService _patientsService;
	
	public PatientsController(PatientsService service, ILogger<PatientsController> logger)
	{
		_patientsService = service;
		_logger = logger;
	}

	[HttpGet("", Name = "GetPatients")]
	public async Task<List<Patient>> Get()
	{
		var res = await _patientsService.GetAsync();
		_logger.LogInformation("Get all items");
		return res;
	}

	[HttpGet("{id:length(24)}", Name = "GetPatient")]
	public async Task<ActionResult<Patient>> Get(string id)
	{
		var item = await _patientsService.GetAsync(id);

		if (item is null)
		{
			return NotFound(new { message = "Item not found" });
		}
		
		_logger.LogInformation("Get item by id {Id}", id);

		return item;
	}

	[HttpPost("", Name = "AddPatient")]
	public async Task<IActionResult> Post(Patient newItem)
	{
		await _patientsService.CreateAsync(newItem);
		var res = CreatedAtAction(nameof(Get), new { id = newItem.Id }, newItem);
		_logger.LogInformation("Create item {Id}", newItem.Id);
		
		return res;
	}

	[HttpPut("{id:length(24)}", Name = "UpdatePatient")]
	public async Task<IActionResult> Update(string id, Patient updatedItem)
	{
		var item = await _patientsService.GetAsync(id);

		if (item is null)
		{
			return NotFound(new { message = "Item not found" });
		}

		updatedItem.Id = item.Id;

		await _patientsService.UpdateAsync(id, updatedItem);
		_logger.LogInformation("Update item {Id}", id);

		return NoContent();
	}

	[HttpDelete("{id:length(24)}", Name = "DeletePatient")]
	public async Task<IActionResult> Delete(string id)
	{
		var item = await _patientsService.GetAsync(id);

		if (item is null)
		{
			return NotFound(new { message = "Item not found" });
		}

		await _patientsService.RemoveAsync(item.Id);
		_logger.LogInformation("Delete item {Id}", id);

		return NoContent();
	}
}
