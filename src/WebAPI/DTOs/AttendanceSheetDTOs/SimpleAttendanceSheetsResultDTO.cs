using System.Collections.Generic;

namespace WebAPI.DTOs.AttendanceSheetDTOs
{
    public class SimpleAttendanceSheetsResultDTO
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public int TotalCount { get; set; }

        public List<SimpleAttendanceSheetDTO> Result { get; set; }
    }
}