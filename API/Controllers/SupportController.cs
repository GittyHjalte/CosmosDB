using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using CsvHelper;
using System.Globalization;
using Core.Models;
using CosmosDB.Services;

namespace CosmosDB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupportController : ControllerBase
{
    private readonly SupportService _supportService;

    public SupportController(SupportService supportService)
    {
        _supportService = supportService;
    }

    [HttpPost("/support-message/upload")]
    public async Task<IActionResult> CreateSupportMessage([FromBody] SupportMessage supportMessage)
    {
        if (supportMessage == null)
            return BadRequest("Support message is null");

        await _supportService.AddSupportMessage(supportMessage);
        return Ok(supportMessage);
    }

    [HttpGet("/support-message")]
    public async Task<ActionResult<IEnumerable<SupportMessage>>> GetSupportMessage([FromQuery] string id)
    {
        var supportMessage = await _supportService.GetSupportMessage(id);
        return supportMessage != null ? Ok(supportMessage) : NotFound();
    }
}