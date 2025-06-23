using System.ComponentModel;

namespace TaskManagementWebApi.Input;

public class TaskReassignmentInput
{
    [DefaultValue("*/2 * * * *")]
    public string CronExpression { get; set; } = "*/2 * * * *";
}
