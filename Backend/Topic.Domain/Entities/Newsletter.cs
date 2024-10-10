using System.Diagnostics.Metrics;
using Topic.Domain.Abstractions;
using Topic.Domain.Base;
using Topic.Domain.Enums;
using Topic.Domain.Validations;

namespace Topic.Domain.Entities;

public class Newsletter : AggregateRoot, IAuditableEntity
{
    public Newsletter()
        : base(Guid.NewGuid())
    {

    }

    private Newsletter(string title, StatusEnum status, string[] keywords)
        : base(Guid.NewGuid())
    {
        Title = title;
        Status = status;
        Keywords = keywords;

        Validate();
    }

    public string Title { get; private set; }

    public string[] Keywords { get; private set; }

    public StatusEnum Status { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public virtual ICollection<NewsletterLink> Links { get; private set; } = [];

    public static Newsletter Create(string title, StatusEnum status, string[] keywords) =>
        new(title, status, keywords);

    public void AddLink(NewsletterLink link) {
        Links.Add(link);
    }

    public void Update(string title, StatusEnum status, string[] keywords)
    {
        Title = title;
        Status = status;
        Keywords = keywords;

        Validate();
    }

    public void UpdateStatus(StatusEnum status) => Status = status;

    protected override bool Validate()
    {
        return OnValidate<NewsletterValidation, Newsletter>();
    }
}
