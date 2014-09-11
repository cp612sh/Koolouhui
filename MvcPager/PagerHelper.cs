namespace Koo.Web.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class PagerHelper
    {
        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList)
        {
            if (pagedList == null)
            {
                return Pager(helper, (PagerOptions) null, (IDictionary<string, object>) null);
            }
            return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, null, ((RouteValueDictionary) null), ((IDictionary<string, object>) null));
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, MvcAjaxOptions ajaxOptions)
        {
            if (pagedList != null)
            {
                return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, null, ((RouteValueDictionary) null), ajaxOptions, ((IDictionary<string, object>) null));
            }
            return Pager(ajax, (PagerOptions) null, (IDictionary<string, object>) null);
        }

        private static MvcHtmlString Pager(AjaxHelper ajax, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            return new PagerBuilder(null, ajax, pagerOptions, htmlAttributes).RenderPager();
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, (IDictionary<string, object>) null);
            }
            return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, null, ((RouteValueDictionary) null), ((IDictionary<string, object>) null));
        }

        private static MvcHtmlString Pager(HtmlHelper helper, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            return new PagerBuilder(helper, null, pagerOptions, htmlAttributes).RenderPager();
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions)
        {
            if (pagedList != null)
            {
                return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, ((RouteValueDictionary) null), ajaxOptions, ((IDictionary<string, object>) null));
            }
            return Pager(ajax, pagerOptions, (IDictionary<string, object>) null);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, htmlAttributes);
            }
            return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, null, null, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, new RouteValueDictionary(htmlAttributes));
            }
            return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, null, null, htmlAttributes);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, pagerOptions, new RouteValueDictionary(htmlAttributes));
            }
            return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, pagerOptions, htmlAttributes);
            }
            return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, string routeName, object routeValues, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, (PagerOptions) null, (IDictionary<string, object>) new RouteValueDictionary(htmlAttributes));
            }
            return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, routeName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, (PagerOptions) null, htmlAttributes);
            }
            return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, routeName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, object routeValues)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, (IDictionary<string, object>) null);
            }
            return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, null);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, (IDictionary<string, object>) null);
            }
            return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, null);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, object routeValues, MvcAjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, (PagerOptions) null, (IDictionary<string, object>) new RouteValueDictionary(htmlAttributes));
            }
            return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, null, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, RouteValueDictionary routeValues, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, (PagerOptions) null, htmlAttributes);
            }
            return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, null, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, object routeValues, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, new RouteValueDictionary(htmlAttributes));
            }
            return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, htmlAttributes);
            }
            return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, object routeValues, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, pagerOptions, new RouteValueDictionary(htmlAttributes));
            }
            return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, RouteValueDictionary routeValues, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, pagerOptions, htmlAttributes);
            }
            return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, PagerOptions pagerOptions, string routeName, object routeValues, object htmlAttributes)
        {
            int totalPageCount = (int) Math.Ceiling((double) (((double) totalItemCount) / ((double) pageSize)));
            PagerBuilder builder = new PagerBuilder(helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
            return builder.RenderPager();
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            int totalPageCount = (int) Math.Ceiling((double) (((double) totalItemCount) / ((double) pageSize)));
            PagerBuilder builder = new PagerBuilder(helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, htmlAttributes);
            return builder.RenderPager();
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, object routeValues, MvcAjaxOptions ajaxOptions, object htmlAttributes)
        {
            int totalPageCount = (int) Math.Ceiling((double) (((double) totalItemCount) / ((double) pageSize)));
            PagerBuilder builder = new PagerBuilder(ajax, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, new RouteValueDictionary(routeValues), ajaxOptions, new RouteValueDictionary(htmlAttributes));
            return builder.RenderPager();
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, RouteValueDictionary routeValues, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            int totalPageCount = (int) Math.Ceiling((double) (((double) totalItemCount) / ((double) pageSize)));
            PagerBuilder builder = new PagerBuilder(ajax, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes);
            return builder.RenderPager();
        }
    }
}

