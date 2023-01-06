using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBlog.Services
{
    public static class Pagination
    {
        public static PaginatedResult<T> PaginateRecords<T>(this IEnumerable<T> records, int page, int pageSize)
          where T : class
        {

            int recordsToSkip = (page - 1) * pageSize;
            int totalRecords = records.Count();
            double totalPages = totalRecords / (double)pageSize;

            int pageCount = int.Parse(Math.Ceiling(totalPages).ToString());

            IEnumerable<T> paginatedRecords = records.Skip(recordsToSkip).Take(pageSize);

            PaginatedResult<T> paginatedResult = new PaginatedResult<T>
            {
                Records = paginatedRecords,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = pageCount,
                TotalRecords = totalRecords
            };
            return paginatedResult;
        }
        public class PaginatedResult<T> where T : class
        {
            public IEnumerable<T> Records { get; set; } = Enumerable.Empty<T>();
            public int PageSize { get; set; }
            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }
            public int TotalRecords { get; set; }
        }
    }
   
}
