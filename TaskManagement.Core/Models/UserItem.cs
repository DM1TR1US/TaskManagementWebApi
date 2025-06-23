namespace TaskManagement.Core.Models;

public class UserItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}
