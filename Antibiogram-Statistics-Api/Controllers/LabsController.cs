using MedicalSystems.Antibiogram.StatisticsApi.Models;
using MedicalSystems.Antibiogram.StatisticsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystems.Antibiogram.StatisticsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LabsController : ControllerBase
{
	private readonly ILogger<LabsController> _logger;
	private readonly LabsService _labsService;
	
	public LabsController(LabsService service, ILogger<LabsController> logger)
	{
		_labsService = service;
		_logger = logger;
	}

	[HttpGet("", Name = "GetLabs")]
	public async Task<List<Lab>> Get()
	{
		var res = await _labsService.GetAsync();
		_logger.LogInformation("Get all items");
		return res;
	}

	[HttpGet("{id:length(24)}", Name ="GetLab")]
	public async Task<ActionResult<Lab>> Get(string id)
	{
		var item = await _labsService.GetAsync(id);

		if (item is null)
		{
			return NotFound(new { message = "Item not found" });
		}
		
		_logger.LogInformation("Get item by id {Id}", id);

		return item;
	}

	[HttpPost("", Name = "AddLab")]
	public async Task<IActionResult> Post(Lab newItem)
	{
		await _labsService.CreateAsync(newItem);
		var res = CreatedAtAction(nameof(Get), new { id = newItem.Id }, newItem);
		_logger.LogInformation("Create item {Id}", newItem.Id);
		
		return res;
	}

	[HttpPut("{id:length(24)}", Name = "UpdateLab")]
	public async Task<IActionResult> Update(string id, Lab updatedItem)
	{
		var item = await _labsService.GetAsync(id);

		if (item is null)
		{
			return NotFound(new { message = "Item not found" });
		}

		updatedItem.Id = item.Id;

		await _labsService.UpdateAsync(id, updatedItem);
		_logger.LogInformation("Update item {Id}", id);

		return NoContent();
	}

	[HttpDelete("{id:length(24)}", Name = "DeleteLab")]
	public async Task<IActionResult> Delete(string id)
	{
		var item = await _labsService.GetAsync(id);

		if (item is null)
		{
			return NotFound(new { message = "Item not found" });
		}

		await _labsService.RemoveAsync(item.Id);
		_logger.LogInformation("Delete item {Id}", id);

		return NoContent();
	}
}
