namespace CityTransport.Web.Common.Helpers.Messages
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text;

    public class MessageTagHelper : TagHelper
    {
        public string Message { get; set; }

        public MessageType Type { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var result = new StringBuilder();
            //result
            //    .Append($"<div class=\"alert alert-{this.Type.ToLower()} alert-dismissible show\">")
            //    .Append("<button type = \"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>")
            //    .Append(this.Message)
            //    .Append("</div>");

            result
                .Append($"<div class=\"alert alert-{this.Type.ToString().ToLower()}\">")
                .Append("<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>")
                .Append(this.Message)
                .Append("</div>");

            output.Content.SetHtmlContent(result.ToString());
        }
    }
}
