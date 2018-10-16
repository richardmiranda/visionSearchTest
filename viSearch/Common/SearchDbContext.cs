using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using viSearch.Models;

namespace viSearch.Common
{
    public class SearchDbContext : DbContext
    {
        public SearchDbContext()
        {
        }

        public SearchDbContext(DbContextOptions<SearchDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<SearchSynonym> SearchSynonyms { get; set; }
    }
}
