using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services;
using Oid85.FinMarket.TraderTinkoff.Application.Services;
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
    /// Загрузить инструменты
    /// </summary>
    [HttpPost("load-instruments")]
    public async Task<IActionResult> LoadInstrumentsAsync()
    {
        await traderService.LoadInstrumentsAsync();
        return Ok();
    }
}