using Common.Output;

namespace Presentation.Output
{
    //centeral dto out for api
    public class ApiResultDto<T> : ResultDto<T>
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
    public class ApiResultDto : ResultDto
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
