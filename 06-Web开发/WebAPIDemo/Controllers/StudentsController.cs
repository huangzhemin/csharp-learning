using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static readonly List<Student> Students = new()
    {
        new Student { Id = 1, Name = "张三", Age = 20, Email = "zhangsan@example.com", Major = "计算机科学" },
        new Student { Id = 2, Name = "李四", Age = 22, Email = "lisi@example.com", Major = "数学" },
        new Student { Id = 3, Name = "王五", Age = 21, Email = "wangwu@example.com", Major = "物理" }
    };

    private readonly ILogger<StudentsController> _logger;

    public StudentsController(ILogger<StudentsController> logger)
    {
        _logger = logger;
    }

    // GET: api/students
    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetStudents()
    {
        _logger.LogInformation("获取所有学生信息");
        return Ok(Students);
    }

    // GET: api/students/5
    [HttpGet("{id}")]
    public ActionResult<Student> GetStudent(int id)
    {
        var student = Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            _logger.LogWarning("未找到ID为 {StudentId} 的学生", id);
            return NotFound($"未找到ID为 {id} 的学生");
        }

        _logger.LogInformation("获取学生信息: {StudentName}", student.Name);
        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public ActionResult<Student> CreateStudent([FromBody] CreateStudentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newStudent = new Student
        {
            Id = Students.Max(s => s.Id) + 1,
            Name = request.Name,
            Age = request.Age,
            Email = request.Email,
            Major = request.Major
        };

        Students.Add(newStudent);
        _logger.LogInformation("创建新学生: {StudentName}", newStudent.Name);

        return CreatedAtAction(nameof(GetStudent), new { id = newStudent.Id }, newStudent);
    }

    // PUT: api/students/5
    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] UpdateStudentRequest request)
    {
        var student = Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound($"未找到ID为 {id} 的学生");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        student.Name = request.Name;
        student.Age = request.Age;
        student.Email = request.Email;
        student.Major = request.Major;

        _logger.LogInformation("更新学生信息: {StudentName}", student.Name);
        return NoContent();
    }

    // DELETE: api/students/5
    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var student = Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound($"未找到ID为 {id} 的学生");
        }

        Students.Remove(student);
        _logger.LogInformation("删除学生: {StudentName}", student.Name);
        return NoContent();
    }

    // GET: api/students/search?major=计算机科学
    [HttpGet("search")]
    public ActionResult<IEnumerable<Student>> SearchStudents([FromQuery] string? major, [FromQuery] int? minAge)
    {
        var query = Students.AsQueryable();

        if (!string.IsNullOrEmpty(major))
        {
            query = query.Where(s => s.Major.Contains(major));
        }

        if (minAge.HasValue)
        {
            query = query.Where(s => s.Age >= minAge.Value);
        }

        var results = query.ToList();
        _logger.LogInformation("搜索学生，条件: major={Major}, minAge={MinAge}, 结果数量: {Count}", 
            major, minAge, results.Count);

        return Ok(results);
    }
}
