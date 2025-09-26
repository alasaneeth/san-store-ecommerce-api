using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Application.ViewModels
{
    public class PaginationVM<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfRecordes { get; set; }
        public List<T> Items { get; set; }
        public bool HasPrevios => CurrentPage > 0;
        public bool HasNext => CurrentPage < TotalPages;

        public PaginationVM(int currentPage, int totalPages, int pageSize, int totalNumberOfRecordes, List<T> items)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalNumberOfRecordes = totalNumberOfRecordes;
            Items = items;

        }

    }
}
