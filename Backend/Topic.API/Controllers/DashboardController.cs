using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Topic.Api.Extensions;
using Topic.Application.UseCases.Dashboards.Queries;

namespace Topic.Api.Controllers;

/// <summary>
/// The <see cref="DashboardController"/> class is an ASP.NET Core API controller for dashboard.
/// It provides endpoints for retrievin dashboard records.
/// </summary>
[ApiController]
[Route("api/dashboard")]
public class DashboardController(IMediator _mediator) : ControllerBase
{
    /// <summary>
    /// Retrieve a newsletter record.
    /// </summary>
    /// <param name="id">The newsletter id to retireve</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Obter sumario dados")]
    [SwaggerResponse(StatusCodes.Status200OK, "Sumário com sucesso")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDashboard(), cancellationToken);
        return result.MatchToResult();
    }
}
