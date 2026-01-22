namespace Presentation.Output.Base
{
    //dto for HATEOAS
    public class LinkDto
    {
        public string Rel { get; set; } = null!;
        public string Href { get; set; } = null!;
        public string Method { get; set; } = null!;

    }
}
