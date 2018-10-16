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

        public static List<SearchSynonym> SearchSynonyms = new List<SearchSynonym> {
            new SearchSynonym { ID = 1, SearchTerm = "Search Term 1", Synonyms = "Search Synonyms 1" },
            new SearchSynonym { ID = 2, SearchTerm = "Search Term 2", Synonyms = "Search Synonyms 2" },
            new SearchSynonym { ID = 3, SearchTerm = "Search Term 3", Synonyms = "Search Synonyms 3" }
        };
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
