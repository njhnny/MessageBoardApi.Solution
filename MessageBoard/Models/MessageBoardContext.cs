using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Models
{
  public class MessageBoardContext : DbContext
  {
    public MessageBoardContext(DbContextOptions<MessageBoardContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<CurrentUser> CurrentUser { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<CurrentUser>()
        .HasData(
          new CurrentUser { CurrentUserId = 1 , PersonId = 0 }
        );
    }
  }
}