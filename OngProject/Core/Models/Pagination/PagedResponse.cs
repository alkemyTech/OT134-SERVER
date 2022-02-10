using OngProject.Core.Helper;
using System;
using System.Collections.Generic;

namespace OngProject.Core.Models.Paged
{
    public class PagedResponse<T>
    {
        public string PreviusPage { get; set; }
        public string NextPage { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public List<T> Items { get; set; }
        public PagedResponse(PagedList<T> pagedList, string url)
        {            
            this.Items = pagedList;
            this.TotalPages = pagedList.TotalPages;
            this.PageSize = pagedList.PageSize;
            this.PageNumber = pagedList.CurrentPage;
            if (pagedList.HasNext) this.NextPage = $"{url}?PageNumber={pagedList.CurrentPage + 1}&PageSize={pagedList.PageSize}";
            if (pagedList.HasPrevius) this.PreviusPage = $"{url}?PageNumber={pagedList.CurrentPage - 1}&PageSize={pagedList.PageSize}";
        }
    }
}
