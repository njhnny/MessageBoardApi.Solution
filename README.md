using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Models
{
  public class User (handled by that interface) : Identity
  {
    public User()
    {
      this.Messages = new HashSet<Message>();
    }

    public int UserId { get; set; }
    usual params
    public string UserBio { get; set; }
    public int PostCount? { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
  }
}

namespace MessageBoard.Models
{
  public class Message
  {
    public Message()
    {
      this.PostDate = DateTime.Now;
    }

    public int MessageId { get; set; }
    public string Header { get; set; }
    public string Body { get; set; }
    public DateTime PostDate { get; set; }
    Public virtual ApplicationUser User { get; set; }
  }
}