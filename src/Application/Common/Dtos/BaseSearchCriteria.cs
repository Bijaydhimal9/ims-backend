using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Dtos
{
    public class BaseSearchCriteria
    {

        public Guid CurrentUserId { get; set; }


        public int Page { get; set; } = 1;


        public int Size { get; set; } = 10;


        private string _search;

        public string Search
        {
            get { return _search; }
            set { _search = value?.Trim().ToLower(); }
        }


        public string SortBy { get; set; }

        public SortType SortType { get; set; }

        public BaseSearchCriteria()
        {
        }
    }
}