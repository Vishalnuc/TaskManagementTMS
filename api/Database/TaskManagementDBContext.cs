using Microsoft.EntityFrameworkCore;

public class TaskManagementDBContext: DbContext {
    public TaskManagementDBContext(DbContextOptions<TaskManagementDBContext> options): base(options) {}
    public DbSet<Task> Tasks {get; set;}
}