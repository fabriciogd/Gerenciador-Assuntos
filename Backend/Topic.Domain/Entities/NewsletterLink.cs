using Topic.Domain.Base;

namespace Topic.Domain.Entities;

public class NewsletterLink : Entity
{
    protected NewsletterLink(string title, string description, string url)
      : base(Guid.NewGuid())
    {
        Title = title;
        Description = description;
        Url = url;
    }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string Url { get; private set; }

    public Guid NewsletterId { get; private set; }

    public virtual Newsletter Newsletter { get; private set; }

    public static NewsletterLink Create(string title, string description, string link) =>
        new(title, description, link);

    protected override bool Validate()
    {
        return true;
    }
}
