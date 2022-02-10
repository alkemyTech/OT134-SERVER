using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.PagedResourceParameters
{
    public class PaginationParams
    {
        const int MaxPageSize = 50;

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        [Range(1, int.MaxValue)]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
