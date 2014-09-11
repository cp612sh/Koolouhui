namespace Koo.Web.UI.Controls
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using System.Web.UI;

    public static class ScriptResourceExtensions
    {
        public static void RegisterMvcPagerScriptResource(this HtmlHelper html)
        {
            Page currentHandler = html.ViewContext.HttpContext.CurrentHandler as Page;
            string webResourceUrl = (currentHandler ?? new Page()).ClientScript.GetWebResourceUrl(typeof(PagerHelper), "MvcPager.min.js");
            html.ViewContext.Writer.Write("<script type=\"text/javascript\" src=\"" + webResourceUrl + "\"></script>");
        }
    }
}

