using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TaskManagement.Core.Models;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public TaskState State { get; set; } = TaskState.Waiting;

    public Guid? AssignedUserId { get; set; }
    public UserItem? AssignedUser { get; set; }

    public List<Guid> AssignmentHistory { get; set; } = new();

    [NotMapped]
    public Guid? CurrentUser => AssignedUserId;

    public bool AssignTo(Guid userId)
    {
        if (!IsValidAssignee(userId))
            return false;

        if (AssignedUserId is Guid currentUser)
            AssignmentHistory.Add(currentUser);

        AssignedUserId = userId;
        AssignedUser = null;
        State = TaskState.InProgress;

        return true;
    }

    public bool WasAssignedToAll(IEnumerable<Guid> allUserIds)
    {
        var distinctAssigned = AssignmentHistory.Distinct().ToHashSet();
        return allUserIds.All(id => distinctAssigned.Contains(id));
    }

    private bool IsValidAssignee(Guid newUserId)
    {
        return newUserId != AssignedUserId
            && (AssignmentHistory.Count < 1 || AssignmentHistory.LastOrDefault() != newUserId);
    }
}
