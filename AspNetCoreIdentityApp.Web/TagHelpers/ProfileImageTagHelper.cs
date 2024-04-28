using AspNetCoreIdentityApp.Web.TagHelpers.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreIdentityApp.Web.TagHelpers
{
    public class ProfileImageTagHelper : TagHelper
    {
        public string? PictureUrl { get; set; }
        public string? TagHelperWidth { get; set; }
        public string? TagHelperHeight { get; set; }
        public string? TagHelperId { get; set; }
        public string? TagHelperClass { get; set; }
        public string? TagHelperStyle { get; set; }
        public string? TagHelperAlt { get; set; }
        public string? TagHelperTitle { get; set; }
        override public void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.SetBaseElementFeatures("img", TagMode.StartTagAndEndTag);
            if (string.IsNullOrEmpty(PictureUrl))
            {
                output.Attributes.SetAttribute("src", "/images/default_profile_image.png");
            }
            else
            {
                output.Attributes.SetAttribute("src", $"/images/{PictureUrl}");
            }
            output.Attributes.SetAttributeIfNotNullOrEmpty("class", TagHelperClass);
            output.Attributes.SetAttributeIfNotNullOrEmpty("width", TagHelperWidth);
            output.Attributes.SetAttributeIfNotNullOrEmpty("height", TagHelperHeight);
            output.Attributes.SetAttributeIfNotNullOrEmpty("alt", TagHelperAlt);
            output.Attributes.SetAttributeIfNotNullOrEmpty("title", TagHelperTitle);
            output.Attributes.SetAttributeIfNotNullOrEmpty("style", TagHelperStyle);
            output.Attributes.SetAttributeIfNotNullOrEmpty("id", TagHelperId);
        }
    }


}
