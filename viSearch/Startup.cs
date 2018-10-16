using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using viSearch.Common;
using viSearch.Models;

namespace viSearch
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SearchDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "viSearchDB"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var context = serviceProvider.GetService<SearchDbContext>();
            SeedData(context);
            app.UseMvc();
        }

        private void SeedData(SearchDbContext context)
        {
            var searchSynonyms = new List<SearchSynonym>();
            for (var i = 1; i <= 10; i++)
            {
                searchSynonyms.Add(new SearchSynonym
                {
                    ID = i,
                    SearchTerm = "Search Term " + i,
                    Synonyms = "Synonym " + i
                });
            }
            context.SearchSynonyms.AddRange(searchSynonyms);
            context.SaveChanges();
        }
    }
}
