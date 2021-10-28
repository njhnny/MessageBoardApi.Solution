using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MessageBoard.Models
{
    public class Person
    {
    public Person()
    {
      this.Messages = new HashSet<Message>();
    }
    public int PersonId { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Maximum amount of characters for the username is 20")]
    public string Name { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
    }
}