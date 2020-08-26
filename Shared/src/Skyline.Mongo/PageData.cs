using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Mongo
{
    public class PageData<T>
    {
        public PageData(int pageIndex, int pageSize, int totalCount, List<T> items)
        {
            Total = totalCount;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Items = items;
            TotalPage = Total % PageSize == 0 ? Total / PageSize : Total / PageSize + 1;
        }
        public int Total { get; }
        public List<T> Items { get; }
        public int PageSize { get; }
        public int PageIndex { get; }
        public int TotalPage { get; }
        public bool HasPrev => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPage;
    }
}
