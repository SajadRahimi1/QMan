using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Comment;

public class Comment:BaseEntity
{
    public string Text { get; set; }
    public int? BusinessId { get; set; }
    public Business.Business Business { get; set; }
    public bool ShowInHome { get; set; } = false;
}