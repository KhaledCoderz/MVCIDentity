using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace MVCIDentity.Helper
{
    public static class HtmlHelper
    {
        public static IHtmlContent CustomTextBoxFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel,TResult>> expression, object htmlAttributes = null)
        {
            var Attributes = HtmlHelper<TModel>.AnonymousObjectToHtmlAttributes(htmlAttributes ?? new { });

            if (Attributes.ContainsKey("class"))
            {
                var currentclass = Attributes["class"].ToString();
                if (!currentclass.Contains(" form-control "))
                {
                    Attributes["class"] += " form-control ";
                }
            }
            else
            {
                Attributes["class"] = " form-control ";
            }


            return htmlHelper.TextBoxFor(expression, htmlAttributes: Attributes);

        }


    }
}
