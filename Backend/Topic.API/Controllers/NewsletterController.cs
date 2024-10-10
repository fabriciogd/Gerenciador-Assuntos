using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Topic.Api.Extensions;
using Topic.Api.Models;
using Topic.Application.Contracts.News;
using Topic.Application.UseCases.Newsletters.Commands;
using Topic.Application.UseCases.Newsletters.Queries;
using Topic.Application.UseCases.Newsletters.Responses;

namespace Topic.Api.Controllers;

/// <summary>
/// The <see cref="NewsletterController"/> class is an ASP.NET Core API controller for managing newsletter.
/// It provides endpoints for creating, retrieving, updating and deleting newsletter records.
/// </summary>
[ApiController]
[Route("api/assuntos")]
public class NewsletterController(IMediator _mediator) : ControllerBase
{
    /// <summary>
    /// Creates a new newsletter record.
    /// </summary>
    /// <param name="command">The command containing the details of the newsletter to create.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Cadastrar assunto")]
    [SwaggerResponse(StatusCodes.Status200OK, "Assunto cadastrado com sucesso", typeof(NewsletterResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Dados duplicados", typeof(ApiResponse))]
    public async Task<IActionResult> Create([FromBody] CreateNewsetter command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchToResult();
    }

    /// <summary>
    /// Update a newsletter record.
    /// </summary>
    /// <param name="id">The newsletter id to update</param>
    /// <param name="command">The command containing the details of the newsletter to update.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpPut("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Atualizar assunto")]
    [SwaggerResponse(StatusCodes.Status200OK, "Assunto atualizado com sucesso", typeof(NewsletterResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Assunto não encontrado", typeof(ApiResponse))]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateNewsetter command, CancellationToken cancellationToken)
    {
        command = command with { Id = id };

        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchToResult();
    }

    /// <summary>
    /// Delete a newsletter record.
    /// </summary>
    /// <param name="id">The newsletter id to delete</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpDelete("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Remover assunto")]
    [SwaggerResponse(StatusCodes.Status200OK, "Assunto removido com sucesso")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Assunto não encontrado", typeof(ApiResponse))]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteNewsetter(id), cancellationToken);
        return result.MatchToResult();
    }

    /// <summary>
    /// Retrieve a newsletter record.
    /// </summary>
    /// <param name="id">The newsletter id to retireve</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Obter assunto por id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Assunto encontrado com sucesso")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Assunto não encontrado", typeof(ApiResponse))]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetNewsletterById(id), cancellationToken);
        return result.MatchToResult();
    }

    /// <summary>
    /// Retrieve all newsletter records.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Obter assuntos")]
    [SwaggerResponse(StatusCodes.Status200OK, "Assuntos encontrados com sucesso")]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllNewsletter(), cancellationToken);
        return result.MatchToResult();
    }

    /// <summary>
    /// Search newsletter.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpPost("search")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Procurar noticias")]
    [SwaggerResponse(StatusCodes.Status200OK, "Busca iniciada com sucesso")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Assunto não encontrado", typeof(ApiResponse))]
    public async Task<IActionResult> Search([FromBody] SearchNewsletter command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchToResult();
    }
}
