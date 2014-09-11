namespace Koo.Web.UI.Controls
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Mvc;

    public static class DisplayNameForExtension
    {
        public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<IPagedList<TModel>> html, Expression<Func<TModel, TValue>> expression)
        {
            return GetDisplayName<TModel, TValue>(expression);
        }

        public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<PagedList<TModel>> html, Expression<Func<TModel, TValue>> expression)
        {
            return GetDisplayName<TModel, TValue>(expression);
        }

        private static MvcHtmlString GetDisplayName<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, new ViewDataDictionary<TModel>());
            string expressionText = ExpressionHelper.GetExpressionText(expression);
            string s = metadata.DisplayName ?? (metadata.PropertyName ?? expressionText.Split(new char[] { '.' }).Last<string>());
            return new MvcHtmlString(HttpUtility.HtmlEncode(s));
        }
    }
}

