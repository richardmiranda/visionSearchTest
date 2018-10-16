﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using viSearch.Models;

namespace viSearch.Controllers
{
    [Route("api/[controller]")]
    public class SearchSynonymController : Controller
    {
        #region Ctors
        public SearchSynonymController()
        {
        }
        #endregion

        #region API: GetList
        [HttpPost("find")]
        public IActionResult GetList(SearchSynonymFindRequest model)
        {
            var entities = SearchSynonym.SearchSynonyms.Skip(model.PageSize * model.PageIndex).Take(model.PageSize);
            if (!string.IsNullOrEmpty(model.SortName))
            {
                switch (model.SortName)
                {
                    case "ID":
                        entities = entities.OrderBy(c => c.ID);
                        break;
                    case "SearchTerm":
                        entities = entities.OrderBy(c => c.SearchTerm);
                        break;
                    case "Synonyms":
                        entities = entities.OrderBy(c => c.Synonyms);
                        break;
                }
            }
            return Json(entities);
        }
        #endregion

        #region API: Add
        [HttpPost("add")]
        public IActionResult Add(SearchSynonymAddRequest model)
        {
            var newData = new SearchSynonym
            {
                ID = SearchSynonym.SearchSynonyms.Count + 1,
                SearchTerm = model.SearchTerm,
                Synonyms = model.Synonyms
            };
            SearchSynonym.SearchSynonyms.Add(newData);
            return Json(newData);
        }
        #endregion

        #region API: Update
        [HttpPost("update")]
        public IActionResult Update(SearchSynonym model)
        {
            var newData = SearchSynonym.SearchSynonyms.Find(c => c.ID == model.ID);
            newData.SearchTerm = model.SearchTerm;
            newData.Synonyms = model.Synonyms;
            return Json(newData);
        }
        #endregion

        #region API: Delete
        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var removedData = SearchSynonym.SearchSynonyms.Find(c => c.ID == id);
            SearchSynonym.SearchSynonyms.Remove(removedData);
            return Json(new { Success = true });
        }
        #endregion
    }
}