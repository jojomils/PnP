using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PnP.Infrastructure {

  public static class UrlExtensions
  {

    public static string PathAndQuery(this HttpRequest request) =>
        request.QueryString.HasValue
            ? $"{request.Path}{request.QueryString}"
            : request.Path.ToString();

    public static IHtmlContent DisabledIf(this IHtmlHelper htmlHelper,
                                             bool condition)
       => new HtmlString(condition ? "disabled=\"disabled\"" : "");


  }
}
