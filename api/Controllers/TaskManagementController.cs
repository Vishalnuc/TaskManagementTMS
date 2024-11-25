using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TaskManagementController : ControllerBase
{
    private readonly TaskManagementDBContext _taskManagementDBContext;
    public TaskManagementController(TaskManagementDBContext dbContext){
        _taskManagementDBContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Task>>> GetAllTasks(){
        return await _taskManagementDBContext.Tasks.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Task>> AddNewTask(Task taskData) {
        _taskManagementDBContext.Tasks.Add(taskData);
        await _taskManagementDBContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAllTasks), new {taskID = taskData.TaskID}, taskData);
    }

    [HttpDelete("{taskID}")]
    public async Task<IActionResult> DeleteTask(int taskID){
        var taskData = await _taskManagementDBContext.Tasks.FindAsync(taskID);
        if (taskData == null)
            return NotFound();
        _taskManagementDBContext.Tasks.Remove(taskData);
        await _taskManagementDBContext.SaveChangesAsync();
        return NoContent();
    }
}