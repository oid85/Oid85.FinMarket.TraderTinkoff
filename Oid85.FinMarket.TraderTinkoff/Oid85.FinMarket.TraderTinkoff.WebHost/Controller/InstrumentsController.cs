using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services;
using Oid85.FinMarket.TraderTinkoff.Core;
using Oid85.FinMarket.TraderTinkoff.Core.Requests;
using Oid85.FinMarket.TraderTinkoff.Core.Responses;
using Oid85.FinMarket.TraderTinkoff.WebHost.Controller.Base;

namespace Oid85.FinMarket.TraderTinkoff.WebHost.Controller;

/// <summary>
/// Инструменты
/// </summary>
[Route("api/instruments")]
[ApiController]
public class InstrumentsController(
    ITraderService traderService)
    : BaseController
{
    /// <summary>
    /// Получить цены
    /// </summary>
    [HttpPost("price")]
    [ProducesResponseType(typeof(BaseResponse<GetInstrumentPriceResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetInstrumentPriceResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetInstrumentPriceResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetInstrumentPriceAsync(
        [FromBody] GetInstrumentPriceRequest request) =>
        GetResponseAsync(
            () => traderService.GetInstrumentPriceAsync(request),
            result => new BaseResponse<GetInstrumentPriceResponse> { Result = result });
}