namespace Koo.Web.UI.Controls
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IPagedList<T> : IEnumerable<T>, IPagedList, IEnumerable
    {
    }
}

