namespace Koo.Web.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    internal class PagerBuilder
    {
        private readonly string _actionName;
        private readonly AjaxHelper _ajax;
        private readonly MvcAjaxOptions _ajaxOptions;
        private readonly bool _ajaxPagingEnabled;
        private readonly string _controllerName;
        private readonly int _endPageIndex;
        private readonly HtmlHelper _html;
        private IDictionary<string, object> _htmlAttributes;
        private readonly int _pageIndex;
        private readonly PagerOptions _pagerOptions;
        private readonly string _routeName;
        private readonly RouteValueDictionary _routeValues;
        private readonly int _startPageIndex;
        private readonly int _totalPageCount;
        private const string CopyrightText = "\r\n<!--MvcPager v2.0 for ASP.NET MVC 4.0+ \x00a9 2009-2013 Webdiyer (http://www.webdiyer.com)-->\r\n";

        internal PagerBuilder(HtmlHelper html, AjaxHelper ajax, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            this._totalPageCount = 1;
            this._startPageIndex = 1;
            this._endPageIndex = 1;
            if (pagerOptions == null)
            {
                pagerOptions = new PagerOptions();
            }
            this._html = html;
            this._ajax = ajax;
            this._pagerOptions = pagerOptions;
            this._htmlAttributes = htmlAttributes;
        }

        internal PagerBuilder(HtmlHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) : this(helper, null, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, null, htmlAttributes)
        {
        }

        internal PagerBuilder(AjaxHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes) : this(null, helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes)
        {
        }

        internal PagerBuilder(HtmlHelper html, AjaxHelper ajax, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            this._totalPageCount = 1;
            this._startPageIndex = 1;
            this._endPageIndex = 1;
            this._ajaxPagingEnabled = ajax != null;
            if (pagerOptions == null)
            {
                pagerOptions = new PagerOptions();
            }
            this._html = html;
            this._ajax = ajax;
            this._actionName = actionName;
            this._controllerName = controllerName;
            if ((pagerOptions.MaxPageIndex == 0) || (pagerOptions.MaxPageIndex > totalPageCount))
            {
                this._totalPageCount = totalPageCount;
            }
            else
            {
                this._totalPageCount = pagerOptions.MaxPageIndex;
            }
            this._pageIndex = pageIndex;
            this._pagerOptions = pagerOptions;
            this._routeName = routeName;
            this._routeValues = routeValues;
            this._ajaxOptions = ajaxOptions;
            this._htmlAttributes = htmlAttributes;
            this._startPageIndex = pageIndex - (pagerOptions.NumericPagerItemCount / 2);
            if ((this._startPageIndex + pagerOptions.NumericPagerItemCount) > this._totalPageCount)
            {
                this._startPageIndex = (this._totalPageCount + 1) - pagerOptions.NumericPagerItemCount;
            }
            if (this._startPageIndex < 1)
            {
                this._startPageIndex = 1;
            }
            this._endPageIndex = (this._startPageIndex + this._pagerOptions.NumericPagerItemCount) - 1;
            if (this._endPageIndex > this._totalPageCount)
            {
                this._endPageIndex = this._totalPageCount;
            }
        }

        private void AddDataAttributes(IDictionary<string, object> attrs)
        {
            attrs.Add("data-urlformat", this.GenerateUrl(0));
            attrs.Add("data-mvcpager", "true");
            if (this._pageIndex > 1)
            {
                attrs.Add("data-firstpage", this.GenerateUrl(1));
            }
            attrs.Add("data-pageparameter", this._pagerOptions.PageIndexParameterName);
            attrs.Add("data-maxpages", this._totalPageCount);
            if (this._pagerOptions.ShowPageIndexBox && (this._pagerOptions.PageIndexBoxType == PageIndexBoxType.TextBox))
            {
                attrs.Add("data-outrangeerrmsg", this._pagerOptions.PageIndexOutOfRangeErrorMessage);
                attrs.Add("data-invalidpageerrmsg", this._pagerOptions.InvalidPageIndexErrorMessage);
            }
        }

        private void AddFirst(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerOptions.FirstPageText, 1, this._pageIndex == 1, PagerItemType.FirstPage);
            if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private void AddLast(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerOptions.LastPageText, this._totalPageCount, this._pageIndex >= this._totalPageCount, PagerItemType.LastPage);
            if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private void AddMoreAfter(ICollection<PagerItem> results)
        {
            if (this._endPageIndex < this._totalPageCount)
            {
                int pageIndex = this._startPageIndex + this._pagerOptions.NumericPagerItemCount;
                if (pageIndex > this._totalPageCount)
                {
                    pageIndex = this._totalPageCount;
                }
                PagerItem item = new PagerItem(this._pagerOptions.MorePageText, pageIndex, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddMoreBefore(ICollection<PagerItem> results)
        {
            if ((this._startPageIndex > 1) && this._pagerOptions.ShowMorePagerItems)
            {
                int pageIndex = this._startPageIndex - 1;
                if (pageIndex < 1)
                {
                    pageIndex = 1;
                }
                PagerItem item = new PagerItem(this._pagerOptions.MorePageText, pageIndex, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddNext(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerOptions.NextPageText, this._pageIndex + 1, this._pageIndex >= this._totalPageCount, PagerItemType.NextPage);
            if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private void AddPageNumbers(ICollection<PagerItem> results)
        {
            for (int i = this._startPageIndex; i <= this._endPageIndex; i++)
            {
                string str = i.ToString(CultureInfo.InvariantCulture);
                if ((i == this._pageIndex) && !string.IsNullOrEmpty(this._pagerOptions.CurrentPageNumberFormatString))
                {
                    str = string.Format(this._pagerOptions.CurrentPageNumberFormatString, str);
                }
                else if (!string.IsNullOrEmpty(this._pagerOptions.PageNumberFormatString))
                {
                    str = string.Format(this._pagerOptions.PageNumberFormatString, str);
                }
                PagerItem item = new PagerItem(str, i, false, PagerItemType.NumericPage);
                results.Add(item);
            }
        }

        private void AddPrevious(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerOptions.PrevPageText, this._pageIndex - 1, this._pageIndex == 1, PagerItemType.PrevPage);
            if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private void AddQueryStringToRouteValues(RouteValueDictionary routeValues, ViewContext viewContext)
        {
            if (routeValues == null)
            {
                routeValues = new RouteValueDictionary();
            }
            NameValueCollection queryString = viewContext.HttpContext.Request.QueryString;
            if ((queryString != null) && (queryString.Count > 0))
            {
                string[] array = new string[] { "x-requested-with", "xmlhttprequest", this._pagerOptions.PageIndexParameterName.ToLower() };
                foreach (string str in queryString.Keys)
                {
                    if (!string.IsNullOrEmpty(str) && (Array.IndexOf<string>(array, str.ToLower()) < 0))
                    {
                        string str2 = queryString[str];
                        routeValues[str] = str2;
                    }
                }
            }
        }

        private string BuildGoToPageSection()
        {
            StringBuilder builder = new StringBuilder();
            if (this._pagerOptions.PageIndexBoxType == PageIndexBoxType.DropDownList)
            {
                int num = this._pageIndex - (this._pagerOptions.MaximumPageIndexItems / 2);
                if ((num + this._pagerOptions.MaximumPageIndexItems) > this._totalPageCount)
                {
                    num = (this._totalPageCount + 1) - this._pagerOptions.MaximumPageIndexItems;
                }
                if (num < 1)
                {
                    num = 1;
                }
                int num2 = (num + this._pagerOptions.MaximumPageIndexItems) - 1;
                if (num2 > this._totalPageCount)
                {
                    num2 = this._totalPageCount;
                }
                builder.AppendFormat("<select data-pageindexbox=\"true\"{0}>", this._pagerOptions.ShowGoButton ? "" : " data-autosubmit=\"true\"");
                for (int i = num; i <= num2; i++)
                {
                    builder.AppendFormat("<option value=\"{0}\"", i);
                    if (i == this._pageIndex)
                    {
                        builder.Append(" selected=\"selected\"");
                    }
                    builder.AppendFormat(">{0}</option>", i);
                }
                builder.Append("</select>");
            }
            else
            {
                builder.AppendFormat("<input type=\"text\" value=\"{0}\" data-pageindexbox=\"true\"{1}/>", this._pageIndex, this._pagerOptions.ShowGoButton ? "" : " data-autosubmit=\"true\"");
            }
            if (!string.IsNullOrEmpty(this._pagerOptions.PageIndexBoxWrapperFormatString))
            {
                builder = new StringBuilder(string.Format(this._pagerOptions.PageIndexBoxWrapperFormatString, builder));
            }
            if (this._pagerOptions.ShowGoButton)
            {
                builder.AppendFormat("<input type=\"button\" data-submitbutton=\"true\" value=\"{0}\"/>", this._pagerOptions.GoButtonText);
            }
            if (!string.IsNullOrEmpty(this._pagerOptions.GoToPageSectionWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
            {
                return string.Format(this._pagerOptions.GoToPageSectionWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, builder);
            }
            return builder.ToString();
        }

        private MvcHtmlString CreateWrappedPagerElement(PagerItem item, string el)
        {
            string str = el;
            switch (item.Type)
            {
                case PagerItemType.FirstPage:
                case PagerItemType.NextPage:
                case PagerItemType.PrevPage:
                case PagerItemType.LastPage:
                    if (!string.IsNullOrEmpty(this._pagerOptions.NavigationPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
                    {
                        str = string.Format(this._pagerOptions.NavigationPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
                    }
                    break;

                case PagerItemType.MorePage:
                    if (!string.IsNullOrEmpty(this._pagerOptions.MorePagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
                    {
                        str = string.Format(this._pagerOptions.MorePagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
                    }
                    break;

                case PagerItemType.NumericPage:
                    if ((item.PageIndex != this._pageIndex) || (string.IsNullOrEmpty(this._pagerOptions.CurrentPagerItemWrapperFormatString) && string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString)))
                    {
                        if (!string.IsNullOrEmpty(this._pagerOptions.NumericPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
                        {
                            str = string.Format(this._pagerOptions.NumericPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
                        }
                        break;
                    }
                    str = string.Format(this._pagerOptions.CurrentPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
                    break;
            }
            return MvcHtmlString.Create(str + this._pagerOptions.PagerItemsSeperator);
        }

        private string GenerateAjaxAnchor(PagerItem item)
        {
            string str = this.GenerateUrl(item.PageIndex);
            if (string.IsNullOrWhiteSpace(str))
            {
                return HttpUtility.HtmlEncode(item.Text);
            }
            TagBuilder builder = new TagBuilder("a") {
                InnerHtml = item.Text
            };
            builder.MergeAttribute("href", str);
            builder.MergeAttribute("data-pageindex", item.PageIndex.ToString(CultureInfo.InvariantCulture));
            return builder.ToString(TagRenderMode.Normal);
        }

        private MvcHtmlString GenerateAjaxPagerElement(PagerItem item)
        {
            if (item.Disabled)
            {
                return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
            }
            return this.CreateWrappedPagerElement(item, this.GenerateAjaxAnchor(item));
        }

        private MvcHtmlString GeneratePagerElement(PagerItem item)
        {
            string str = this.GenerateUrl(item.PageIndex);
            if (item.Disabled)
            {
                return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
            }
            return this.CreateWrappedPagerElement(item, string.IsNullOrEmpty(str) ? HttpUtility.HtmlEncode(item.Text) : string.Format("<a href=\"{0}\">{1}</a>", str, item.Text));
        }

        private string GenerateUrl(int pageIndex)
        {
            string str2;
            ViewContext viewContext = (this._ajax == null) ? this._html.ViewContext : this._ajax.ViewContext;
            if ((pageIndex > this._totalPageCount) || (pageIndex == this._pageIndex))
            {
                return null;
            }
            RouteValueDictionary routeValues = new RouteValueDictionary(viewContext.RouteData.Values);
            this.AddQueryStringToRouteValues(routeValues, viewContext);
            if ((this._routeValues != null) && (this._routeValues.Count > 0))
            {
                foreach (KeyValuePair<string, object> pair in this._routeValues)
                {
                    if (!routeValues.ContainsKey(pair.Key))
                    {
                        routeValues.Add(pair.Key, pair.Value);
                    }
                    else
                    {
                        routeValues[pair.Key] = pair.Value;
                    }
                }
            }
            object obj2 = viewContext.RouteData.Values[this._pagerOptions.PageIndexParameterName];
            string firstPageRouteName = this._routeName;
            if (pageIndex == 0)
            {
                routeValues[this._pagerOptions.PageIndexParameterName] = "__" + this._pagerOptions.PageIndexParameterName + "__";
            }
            else if (pageIndex == 1)
            {
                if (!string.IsNullOrWhiteSpace(this._pagerOptions.FirstPageRouteName))
                {
                    firstPageRouteName = this._pagerOptions.FirstPageRouteName;
                    routeValues.Remove(this._pagerOptions.PageIndexParameterName);
                    viewContext.RouteData.Values.Remove(this._pagerOptions.PageIndexParameterName);
                }
                else
                {
                    Route route = viewContext.RouteData.Route as Route;
                    if ((route != null) && ((route.Defaults[this._pagerOptions.PageIndexParameterName] == UrlParameter.Optional) || !route.Url.Contains("{" + this._pagerOptions.PageIndexParameterName + "}")))
                    {
                        routeValues.Remove(this._pagerOptions.PageIndexParameterName);
                        viewContext.RouteData.Values.Remove(this._pagerOptions.PageIndexParameterName);
                    }
                    else
                    {
                        routeValues[this._pagerOptions.PageIndexParameterName] = pageIndex;
                    }
                }
            }
            else
            {
                routeValues[this._pagerOptions.PageIndexParameterName] = pageIndex;
            }
            RouteCollection routeCollection = (this._ajax == null) ? this._html.RouteCollection : this._ajax.RouteCollection;
            if (!string.IsNullOrEmpty(firstPageRouteName))
            {
                str2 = UrlHelper.GenerateUrl(firstPageRouteName, this._actionName, this._controllerName, routeValues, routeCollection, viewContext.RequestContext, false);
            }
            else
            {
                str2 = UrlHelper.GenerateUrl(null, this._actionName, this._controllerName, routeValues, routeCollection, viewContext.RequestContext, false);
            }
            if (obj2 != null)
            {
                viewContext.RouteData.Values[this._pagerOptions.PageIndexParameterName] = obj2;
            }
            return str2;
        }

        internal MvcHtmlString RenderPager()
        {
            if ((this._totalPageCount <= 1) && this._pagerOptions.AutoHide)
            {
                return MvcHtmlString.Create("\r\n<!--MvcPager v2.0 for ASP.NET MVC 4.0+ \x00a9 2009-2013 Webdiyer (http://www.webdiyer.com)-->\r\n");
            }
            if (((this._pageIndex > this._totalPageCount) && (this._totalPageCount > 0)) || (this._pageIndex < 1))
            {
                return MvcHtmlString.Create(string.Format("{0}<div style=\"color:red;font-weight:bold\">{1}</div>{0}", "\r\n<!--MvcPager v2.0 for ASP.NET MVC 4.0+ \x00a9 2009-2013 Webdiyer (http://www.webdiyer.com)-->\r\n", this._pagerOptions.PageIndexOutOfRangeErrorMessage));
            }
            List<PagerItem> results = new List<PagerItem>();
            if (this._pagerOptions.ShowFirstLast)
            {
                this.AddFirst(results);
            }
            if (this._pagerOptions.ShowPrevNext)
            {
                this.AddPrevious(results);
            }
            if (this._pagerOptions.ShowNumericPagerItems)
            {
                if (this._pagerOptions.AlwaysShowFirstLastPageNumber && (this._startPageIndex > 1))
                {
                    results.Add(new PagerItem("1", 1, false, PagerItemType.NumericPage));
                }
                if (this._pagerOptions.ShowMorePagerItems && ((!this._pagerOptions.AlwaysShowFirstLastPageNumber && (this._startPageIndex > 1)) || (this._pagerOptions.AlwaysShowFirstLastPageNumber && (this._startPageIndex > 2))))
                {
                    this.AddMoreBefore(results);
                }
                this.AddPageNumbers(results);
                if (this._pagerOptions.ShowMorePagerItems && ((!this._pagerOptions.AlwaysShowFirstLastPageNumber && (this._endPageIndex < this._totalPageCount)) || (this._pagerOptions.AlwaysShowFirstLastPageNumber && (this._totalPageCount > (this._endPageIndex + 1)))))
                {
                    this.AddMoreAfter(results);
                }
                if (this._pagerOptions.AlwaysShowFirstLastPageNumber && (this._endPageIndex < this._totalPageCount))
                {
                    results.Add(new PagerItem(this._totalPageCount.ToString(CultureInfo.InvariantCulture), this._totalPageCount, false, PagerItemType.NumericPage));
                }
            }
            if (this._pagerOptions.ShowPrevNext)
            {
                this.AddNext(results);
            }
            if (this._pagerOptions.ShowFirstLast)
            {
                this.AddLast(results);
            }
            StringBuilder builder = new StringBuilder();
            if (this._ajaxPagingEnabled)
            {
                foreach (PagerItem item in results)
                {
                    builder.Append(this.GenerateAjaxPagerElement(item));
                }
            }
            else
            {
                foreach (PagerItem item2 in results)
                {
                    builder.Append(this.GeneratePagerElement(item2));
                }
            }
            TagBuilder builder2 = new TagBuilder(this._pagerOptions.ContainerTagName);
            if (!string.IsNullOrEmpty(this._pagerOptions.Id))
            {
                builder2.GenerateId(this._pagerOptions.Id);
            }
            if (!string.IsNullOrEmpty(this._pagerOptions.CssClass))
            {
                builder2.AddCssClass(this._pagerOptions.CssClass);
            }
            if (!string.IsNullOrEmpty(this._pagerOptions.HorizontalAlign))
            {
                string str = "text-align:" + this._pagerOptions.HorizontalAlign.ToLower();
                if (this._htmlAttributes == null)
                {
                    RouteValueDictionary dictionary = new RouteValueDictionary();
                    dictionary.Add("style", str);
                    this._htmlAttributes = dictionary;
                }
                else if (this._htmlAttributes.Keys.Contains("style"))
                {
                    IDictionary<string, object> dictionary4;
                    (dictionary4 = this._htmlAttributes)["style"] = dictionary4["style"] + ";" + str;
                }
            }
            builder2.MergeAttributes<string, object>(this._htmlAttributes, true);
            if (this._ajaxPagingEnabled)
            {
                IDictionary<string, object> attrs = this._ajaxOptions.ToUnobtrusiveHtmlAttributes();
                attrs.Remove("data-ajax-url");
                attrs.Remove("data-ajax-mode");
                if (this._ajaxOptions.EnablePartialLoading)
                {
                    attrs.Add("data-ajax-partialloading", "true");
                }
                if (this._pageIndex > 1)
                {
                    attrs.Add("data-ajax-currentpage", this._pageIndex);
                }
                if (!string.IsNullOrWhiteSpace(this._ajaxOptions.DataFormId))
                {
                    attrs.Add("data-ajax-dataformid", "#" + this._ajaxOptions.DataFormId);
                }
                this.AddDataAttributes(attrs);
                builder2.MergeAttributes<string, object>(attrs, true);
            }
            if (this._pagerOptions.ShowPageIndexBox)
            {
                if (!this._ajaxPagingEnabled)
                {
                    Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                    this.AddDataAttributes(dictionary3);
                    builder2.MergeAttributes<string, object>(dictionary3, true);
                }
                builder.Append(this.BuildGoToPageSection());
            }
            else
            {
                builder.Length -= this._pagerOptions.PagerItemsSeperator.Length;
            }
            builder2.InnerHtml = builder.ToString();
            return MvcHtmlString.Create("\r\n<!--MvcPager v2.0 for ASP.NET MVC 4.0+ \x00a9 2009-2013 Webdiyer (http://www.webdiyer.com)-->\r\n" + builder2.ToString(TagRenderMode.Normal) + "\r\n<!--MvcPager v2.0 for ASP.NET MVC 4.0+ \x00a9 2009-2013 Webdiyer (http://www.webdiyer.com)-->\r\n");
        }
    }
}

