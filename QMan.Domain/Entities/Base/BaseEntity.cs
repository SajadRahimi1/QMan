using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMan.Domain.Entities.Base;

public class BaseEntity
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; }

    public DateTime? CreatedDateTime { get; set; }=DateTime.Now;
    
    public DateTime? UpdateDateTime { get; set; }
}