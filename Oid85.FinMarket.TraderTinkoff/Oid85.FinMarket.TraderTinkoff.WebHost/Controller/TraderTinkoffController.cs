using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services;
using Oid85.FinMarket.TraderTinkoff.Core;
using Oid85.FinMarket.TraderTinkoff.Core.Requests;
using Oid85.FinMarket.TraderTinkoff.Core.Responses;
using Oid85.FinMarket.TraderTinkoff.WebHost.Controller.Base;

namespace Oid85.FinMarket.TraderTinkoff.WebHost.Controller;

/// <summary>
/// Трейдер
/// </summary>
[Route("api/trader-tinkoff")]
[ApiController]
public class TraderTinkoffController(
    ITraderService traderService)
    : BaseController
{
    /// <summary>
    /// Получить данные о портфеле
    /// </summary>
    [HttpPost("portfolio-info")]
    [ProducesResponseType(typeof(BaseResponse<PortfolioInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<PortfolioInfoResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<PortfolioInfoResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetPortfolioInfoAsync(
        [FromBody] PortfolioInfoRequest request) =>
        GetResponseAsync(
            () => traderService.GetPortfolioInfoAsync(request),
            result => new BaseResponse<PortfolioInfoResponse> { Result = result });

    /// <summary>
    /// Получить список заданий Outbox
    /// </summary>
    [HttpPost("task/list")]
    [ProducesResponseType(typeof(BaseResponse<OutboxTaskListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<OutboxTaskListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<OutboxTaskListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetTaskListAsync(
        [FromBody] OutboxTaskListRequest request) =>
        GetResponseAsync(
            () => traderService.GetOutboxTaskListAsync(request),
            result => new BaseResponse<OutboxTaskListResponse> { Result = result });
}