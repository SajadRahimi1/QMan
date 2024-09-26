using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Comment;

public class Comment:BaseEntity
{
    public string Text { get; set; }
    // later: connect to business 
    public bool ShowInHome { get; set; } = false;
}