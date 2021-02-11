using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.DataContext;

namespace DataAccessLayer.DataContext
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<ShoppingzillaDbContext>
    {
        public ShoppingzillaDbContext CreateDbContext(string[] args)
        {
            AppConfiguration appConfig = new AppConfiguration();
            var opsBuilder = new DbContextOptionsBuilder<ShoppingzillaDbContext>();
            opsBuilder.UseSqlServer(appConfig.connectionstring);
            return new ShoppingzillaDbContext(opsBuilder.Options);
        }
    }
}