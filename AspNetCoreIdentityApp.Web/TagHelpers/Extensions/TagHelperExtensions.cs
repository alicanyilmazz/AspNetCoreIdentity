using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreIdentityApp.Web.TagHelpers.Extensions
{
    public static class TagHelperExtensions
    {
        public static void SetBaseElementFeatures(this TagHelperOutput? output, string tagName, TagMode tagMode = TagMode.StartTagAndEndTag)
        {
            if (output is not null && !string.IsNullOrEmpty(tagName))
            {
                output.TagName = tagName;
            }
            if (output is not null && !string.IsNullOrEmpty(tagName))
            {
                output.TagMode = tagMode;
            }
        }
        public static void SetAttributeIfNotNullOrEmpty(this TagHelperAttributeList? attributes, string attributeName, string? attributeValue)
        {
            if (attributes is not null && !string.IsNullOrEmpty(attributeValue))
            {
                attributes.SetAttribute(attributeName, attributeValue);
            }
        }
    }
}
