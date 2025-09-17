using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "姓名是必填项")]
    [StringLength(50, ErrorMessage = "姓名长度不能超过50个字符")]
    public string Name { get; set; } = string.Empty;

    [Range(16, 100, ErrorMessage = "年龄必须在16-100之间")]
    public int Age { get; set; }

    [Required(ErrorMessage = "邮箱是必填项")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "专业是必填项")]
    [StringLength(100, ErrorMessage = "专业名称长度不能超过100个字符")]
    public string Major { get; set; } = string.Empty;
}

public class CreateStudentRequest
{
    [Required(ErrorMessage = "姓名是必填项")]
    [StringLength(50, ErrorMessage = "姓名长度不能超过50个字符")]
    public string Name { get; set; } = string.Empty;

    [Range(16, 100, ErrorMessage = "年龄必须在16-100之间")]
    public int Age { get; set; }

    [Required(ErrorMessage = "邮箱是必填项")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "专业是必填项")]
    [StringLength(100, ErrorMessage = "专业名称长度不能超过100个字符")]
    public string Major { get; set; } = string.Empty;
}

public class UpdateStudentRequest
{
    [Required(ErrorMessage = "姓名是必填项")]
    [StringLength(50, ErrorMessage = "姓名长度不能超过50个字符")]
    public string Name { get; set; } = string.Empty;

    [Range(16, 100, ErrorMessage = "年龄必须在16-100之间")]
    public int Age { get; set; }

    [Required(ErrorMessage = "邮箱是必填项")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "专业是必填项")]
    [StringLength(100, ErrorMessage = "专业名称长度不能超过100个字符")]
    public string Major { get; set; } = string.Empty;
}
