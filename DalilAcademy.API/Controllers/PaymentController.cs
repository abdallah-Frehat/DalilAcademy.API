using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DalilAcademy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] 
public class PaymentsController(IPaymentService paymentService) : ControllerBase
{
    [HttpPost("create-order")]
    public async Task<ActionResult> CreateOrder(CreateOrderRequestDto request)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized("User ID is missing or invalid in token");
        }

        try
        {
            var result = await paymentService.CreateOrderAsync(userId, request);
            return Ok(result);
        }
        catch (ConflictException ex) 
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("capture-order")]
    public async Task<IActionResult> CaptureOrder([FromBody] CaptureOrderRequestDto request)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized("User ID is missing or invalid in token");
        }

        bool isSuccess = await paymentService.CaptureOrderAsync(userId, request);
        if (!isSuccess)
        {
            return BadRequest(new { Message = "Payment capture failed by PayPal" });
        }

        return Ok(new { Message = "Payment captured successfully and course enrolled" });
    }
}