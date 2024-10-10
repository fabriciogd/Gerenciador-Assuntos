using FluentValidation;
using Topic.Domain.Extensions;
using Topic.Domain.Primitives;

namespace Topic.Domain.Base;

/// <summary>
/// Represents the entity
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected Entity(Guid id) => Id = id;

    /// <summary>
    /// Gets or sets the entity identifier.
    /// </summary>
    public Guid Id { get; private set; }

    public List<ValidationError> Errors = new();

    protected void AddErrors(IReadOnlyCollection<ValidationError> errors)
       => Errors.AddRange(errors);

    protected bool OnValidate<TValidator, TEntity>()
        where TValidator : AbstractValidator<TEntity>, new()
        where TEntity : Entity
    {
        var validationResult = new TValidator().Validate((TEntity)this);
        AddErrors(validationResult.AsErrors());

        return validationResult.IsValid;
    }
    public bool IsValid => Errors.Count == 0;

    protected abstract bool Validate();
}