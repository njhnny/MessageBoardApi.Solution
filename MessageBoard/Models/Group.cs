using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MessageBoard.Models
{
  public class Group
  {
    public Group()
    {
      this.Messages = new HashSet<Message>();
    }
    public int GroupId { get; set; }
    [Required]
    [StringLength(30, ErrorMessage = "Maximum amount of characters for the group name is 30")]
    public string Name { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
  }
}