namespace Fochso.Models.Teacher;

public class TeacherResponseModel : BaseResponseModel
{
    public TeacherViewModel Data { get; set; }
}

public class TeachersResponseModel : BaseResponseModel
{
    public List<TeacherViewModel> Data { get; set; }
}
