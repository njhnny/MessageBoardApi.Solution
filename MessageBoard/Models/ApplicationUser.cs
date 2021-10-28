using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Models
{
    public class User
    {
    public int UserId { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Maximum amount of characters for the username is 20")]
    public string UserName { get; set; }
    [Required]

    public int PostCount { get; set; }
    }
}