using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DalilAcademy.Core.DTOs.Currency;

namespace DalilAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController(ICurrencyService currencyService) : ControllerBase
    {
        [HttpPost("convert")]
        public async Task<ActionResult<ConvertPriceResponseDTO>> Convert(ConvertPriceRequestDTO request)
        {
            return Ok (await currencyService.ConvertAsync(request));
        }
    }
}
