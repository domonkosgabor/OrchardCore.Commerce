using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OrchardCore.Commerce.TagHelpers;

[HtmlTargetElement("mvc-title", Attributes = "text")]
public class MvcTitleTagHelper : TagHelper
{
    private readonly IContentItemDisplayManager _contentItemDisplayManager;
    private readonly IDisplayHelper _displayHelper;

    [HtmlAttributeName("text")]
    public IHtmlContent Text { get; set; }
    public MvcTitleTagHelper(IContentItemDisplayManager contentItemDisplayManager, IDisplayHelper displayHelper)
    {
        _contentItemDisplayManager = contentItemDisplayManager;
        _displayHelper = displayHelper;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var stringBuilder = new StringBuilder();
        await using (var writer = new StringWriter(stringBuilder)) Text.WriteTo(writer, NullHtmlEncoder.Default);

        var shape = await _contentItemDisplayManager.BuildMvcTitleAsync(stringBuilder.ToString());
        var content = await _displayHelper.ShapeExecuteAsync(shape);

        output.TagName = null;
        output.TagMode = TagMode.StartTagAndEndTag;
        output.PostContent.SetHtmlContent(content);
    }
}
