using System.Collections.Generic;

namespace WebAPI.DTOs.EnrolmentDTOs
{
    public class EnrolmentsResultDTO
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public int TotalCount { get; set; }

        public List<EnrolmentDTO> Result { get; set; }
    }
}