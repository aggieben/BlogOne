using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogOne.Web.Extensions;

namespace BlogOne.Web.Helpers
{
    public static class NavHelper
    {
        public static IHtmlString NavLink(this HtmlHelper helper, string action, string controller, string text, string glyph, bool iconLeft = false)
        {
            return NavLink(helper, action, controller, null, text, glyph, iconLeft);
        }

        public static IHtmlString NavLink(this HtmlHelper helper, string action, string controller, object routeValues, string text, string glyph, bool iconLeft = false)
        {
            var url = new UrlHelper(helper.ViewContext.RequestContext);
            string anchor = String.Empty;

            if (helper.ViewContext.RouteData.Values["controller"].Equals(controller) && helper.ViewContext.RouteData.Values["action"].Equals(action))
            {
                anchor += "<a class=\"nav-link active\">";
            }
            else
            {
                string href = (routeValues.HasValue()) ? url.Action(action, controller, routeValues) : url.Action(action, controller);
                anchor += "<a href=\"{0}\" class=\"nav-link\">".f(href);
            }

            string icon = null;
            if (glyph.HasValue())
            {
                icon = "<span class=\"{0}\"></span>".f("glyphicon " + glyph);
            }

            if (iconLeft)
                return new HtmlString("{0}{1}<span>{2}</span></a>".f(anchor, icon, text));
            else
                return new HtmlString("{0}<span>{2}</span>{1}</a>".f(anchor, icon, text));
        }
    }
}