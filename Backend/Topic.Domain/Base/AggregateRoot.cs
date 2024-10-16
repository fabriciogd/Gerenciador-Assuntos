﻿namespace Topic.Domain.Base;

/// <summary>
/// Represents the aggregate root.
/// </summary>
public abstract class AggregateRoot : Entity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
    /// </summary>
    /// <param name="id">The aggregate root identifier.</param>
    protected AggregateRoot(Guid id) : base(id)
    {
    }
}
