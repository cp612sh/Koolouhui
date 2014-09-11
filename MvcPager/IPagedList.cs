namespace Koo.Web.UI.Controls
{
    using System;
    using System.Collections;

    public interface IPagedList : IEnumerable
    {
        int CurrentPageIndex { get; set; }

        int PageSize { get; set; }

        int TotalItemCount { get; set; }
    }
}

