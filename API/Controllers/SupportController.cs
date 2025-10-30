using System.Globalization;
using System.Net;
using System.Net.Http;
using Api.Filters;
using API.Services;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Share.Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupportMessageController : ControllerBase
{
    private readonly SupportService _service;
public SupportMessageController(SupportService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> PostSupportMessage([FromBody] SupportMessage message)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (message == null)
            {
                return BadRequest("Message is null (JSON binding failed)");
            }

            await _service.AddSupportMessage(message);
            return Ok("Support message saved to the database.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Server error: {ex.Message}");
        }
    }
    [HttpGet("support-message")]
    public async Task<ActionResult<IEnumerable<SupportMessage>>> GetSupportMessage([FromQuery] string? category = null)
    {
        var supportMessages = await _service.GetSupportMessage(category);
        return Ok(supportMessages);
    }



}