using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace viSearch.Models
{
    public class SearchSynonym
    {
        public int ID { get; set; }
        public string SearchTerm { get; set; }
        public string Synonyms { get; set; }
    }

    public class SearchSynonymFindRequest
    {
        /// <summary>
        /// Get or set page Index.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Get or set the page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Get or set the sort name 
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// Get or set the sort direction
        /// </summary>
        public bool Ascending { get; set; }
    }

    public class SearchSynonymAddRequest
    {
        public string SearchTerm { get; set; }
        public string Synonyms { get; set; }
    }
}
