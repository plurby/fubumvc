using FubuMVC.UI.Tags;
using HtmlTags;

namespace FubuMVC.UI.Configuration
{
    public class DefaultHtmlConventions : HtmlConventionRegistry
    {
        public DefaultHtmlConventions()
        {
            Editors.IfPropertyIs<bool>().BuildBy(TagActionExpression.BuildCheckbox);

            // Relax, this is just the default fall through action
            Editors.Always.BuildBy(TagActionExpression.BuildTextbox);

            Editors.Always.Modify(AddElementName);
            Displays.Always.BuildBy(req => new HtmlTag("span").Text(req.StringValue()));
            Labels.Always.BuildBy(req => new HtmlTag("span").Text(req.Accessor.Name));

            BeforePartial.Always.BuildBy(req => new NoTag());
            AfterPartial.Always.BuildBy(req => new NoTag());
            BeforeEachOfPartial.Always.BuildBy((req, index, count) => new NoTag());
            AfterEachOfPartial.Always.BuildBy((req, index, count) => new NoTag());
        }

        public static void AddElementName(ElementRequest request, HtmlTag tag)
        {
            if (tag.IsInputElement())
            {
                tag.Attr("name", request.ElementId);
            }
        }
    }
}