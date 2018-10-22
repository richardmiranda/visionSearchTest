using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using viSearch.Common;
using viSearch.Models;

namespace viSearch.Controllers
{
    [Route("api/[controller]")]
    public class SearchSynonymController : Controller
    {
        private SearchDbContext DbContext { get; set; }
        #region Ctors
        public SearchSynonymController(SearchDbContext _context)
        {
            DbContext = _context;
        }
        #endregion

        #region API: Find
        [HttpPost("find")]
        public IActionResult Find(SearchSynonymFindRequest model)
        {
            var totalCount = DbContext.SearchSynonyms.Count();
            var entities = DbContext.SearchSynonyms.Skip(model.PageSize * (model.PageIndex - 1)).Take(model.PageSize);
            if (!string.IsNullOrEmpty(model.SortName))
            {
                switch (model.SortName)
                {
                    case "ID":
                        entities = model.Ascending ? entities.OrderBy(c => c.ID) : entities.OrderByDescending(c => c.ID);
                        break;
                    case "SearchTerm":
                        entities = model.Ascending ? entities.OrderBy(c => c.SearchTerm) : entities.OrderByDescending(c => c.SearchTerm);
                        break;
                    case "Synonyms":
                        entities = model.Ascending ? entities.OrderBy(c => c.Synonyms) : entities.OrderByDescending(c => c.Synonyms);
                        break;
                }
            }
            return Json(new
            {
                PagingList = new
                {
                    TotalResults = totalCount,
                    HasNext = false,
                    Content = entities
                }
            });
        }

        [HttpPost("get")]
        public IActionResult Get(int id)
        {
            var entity = DbContext.SearchSynonyms.Find(id);
            return Json(new { SearchSynonym = entity });
        }
        #endregion

        #region API: Add
        [HttpPost("add")]
        public IActionResult Add(SearchSynonymAddRequest model)
        {
            var maxCount = DbContext.SearchSynonyms.Select(c => c.ID).Max();
            var newData = new SearchSynonym
            {
                ID = maxCount + 1,
                SearchTerm = model.SearchTerm,
                Synonyms = model.Synonyms
            };
            DbContext.SearchSynonyms.Add(newData);
            DbContext.SaveChanges();
            return Json(new { SearchSynonym = newData });
        }
        #endregion

        #region API: Update
        [HttpPost("update")]
        public IActionResult Update(SearchSynonym model)
        {
            var newData = DbContext.SearchSynonyms.Find(model.ID);
            newData.SearchTerm = model.SearchTerm;
            newData.Synonyms = model.Synonyms;
            DbContext.SaveChanges();
            return Json(new { SearchSynonym = newData });
        }
        #endregion

        #region API: Delete
        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var removedData = DbContext.SearchSynonyms.Find(id);
            DbContext.SearchSynonyms.Remove(removedData);
            DbContext.SaveChanges();
            return Json(new { Success = true });
        }

        [HttpPost("bulkdelete")]
        public IActionResult BulkDelete(string ids)
        {
            var idArray = ids.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)).ToList();
            foreach (var id in idArray)
            {
                var removedData = DbContext.SearchSynonyms.Find(id);
                DbContext.SearchSynonyms.Remove(removedData);
            }
            DbContext.SaveChanges();

            return Json(new { SuccessCount = idArray.Count, FailCount = 0 });
        }
        #endregion
    }
}