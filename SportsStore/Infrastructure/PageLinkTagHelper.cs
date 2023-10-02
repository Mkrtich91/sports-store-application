using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure
{
   [HtmlTargetElement("div", Attributes = "page-model")]
   public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;

        public bool PageClassesEnabled { get; set; }

        public string PageClass { get; set; } = string.Empty;

        public string PageClassNormal { get; set; } = string.Empty;

        public string PageClassSelected { get; set; } = string.Empty;

#pragma warning disable SA1201
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
#pragma warning restore SA1201
        {
            this.urlHelperFactory = helperFactory;
        }

        public string? PageRoute { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }

        public PagingInfo? PageModel { get; set; }

        public string? PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (this.ViewContext != null && this.PageModel != null)
            {
                IUrlHelper urlHelper = this.urlHelperFactory.GetUrlHelper(this.ViewContext);
                TagBuilder result = new TagBuilder("div");
                for (int i = 1; i <= this.PageModel.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.Attributes["href"] = urlHelper.Action(
                        this.PageAction,
                        new { productPage = i });
                    _ = tag.InnerHtml.Append(i.ToString(CultureInfo.InvariantCulture));
                    result.InnerHtml.AppendHtml(tag);
                    this.PageUrlValues[key: "productPage"] = i;
                    tag.Attributes[key: "href"] = urlHelper.Action(action: this.PageAction, values: this.PageUrlValues);
#pragma warning disable S4143
                    tag.Attributes[key: "href"] = urlHelper.RouteUrl(routeName: this.PageRoute, values: this.PageUrlValues);
#pragma warning restore S4143

                    if (this.PageClassesEnabled)
                    {
                        tag.AddCssClass(this.PageClass);
                        tag.AddCssClass(i == this.PageModel.CurrentPage
                            ? this.PageClassSelected : this.PageClassNormal);
                    }
                }

                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
