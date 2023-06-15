namespace Fochso.Models.Class;

public class ClassResponseModel : BaseResponseModel
{
    public ClassViewModel Data { get; set; }
}

public class ClassesResponseModel : BaseResponseModel
{
    public List<ClassViewModel> Data { get; set; }
}
