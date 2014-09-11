namespace Koo.Web.UI.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PagedList<T> : List<T>, IPagedList<T>, IEnumerable<T>, IPagedList, IEnumerable
    {
        public PagedList(IEnumerable<T> allItems, int pageIndex, int pageSize)
        {
            this.PageSize = pageSize;
            IList<T> source = (allItems as IList<T>) ?? allItems.ToList<T>();
            this.TotalItemCount = source.Count<T>();
            this.CurrentPageIndex = pageIndex;
            base.AddRange(source.Skip<T>((this.StartRecordIndex - 1)).Take<T>(pageSize));
        }

        public PagedList(IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            int count = (pageIndex - 1) * pageSize;
            base.AddRange(allItems.Skip<T>(count).Take<T>(pageSize));
            this.TotalItemCount = allItems.Count<T>();
            this.CurrentPageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public PagedList(IEnumerable<T> currentPageItems, int pageIndex, int pageSize, int totalItemCount)
        {
            base.AddRange(currentPageItems);
            this.TotalItemCount = totalItemCount;
            this.CurrentPageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public PagedList(IQueryable<T> currentPageItems, int pageIndex, int pageSize, int totalItemCount)
        {
            base.AddRange(currentPageItems);
            this.TotalItemCount = totalItemCount;
            this.CurrentPageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public int CurrentPageIndex { get; set; }

        public int EndRecordIndex
        {
            get
            {
                if (this.TotalItemCount <= (this.CurrentPageIndex * this.PageSize))
                {
                    return this.TotalItemCount;
                }
                return (this.CurrentPageIndex * this.PageSize);
            }
        }

        public int PageSize { get; set; }

        public int StartRecordIndex
        {
            get
            {
                return (((this.CurrentPageIndex - 1) * this.PageSize) + 1);
            }
        }

        public int TotalItemCount { get; set; }

        public int TotalPageCount
        {
            get
            {
                return (int) Math.Ceiling((double) (((double) this.TotalItemCount) / ((double) this.PageSize)));
            }
        }
    }
}

