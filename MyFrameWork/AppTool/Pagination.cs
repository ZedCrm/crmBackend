using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameWork.AppTool
{
    public class Pagination
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public string SortBy { get; set; } = "Id";
        public bool SortDirection { get; set; } = true;

        public int CalculateSkip()
        {
            return (PageNumber - 1) * PageSize; // محاسبه مقدار skip  
        }

        // متدی برای محاسبه تعداد کل صفحات با توجه به تعداد کل رکوردها  
        public int CalculateTotalPages(int totalRecords)
        {
            return (int)Math.Ceiling((double)totalRecords / PageSize); // محاسبه تعداد کل صفحات  
        }
    }
}
