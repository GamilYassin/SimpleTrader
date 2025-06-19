using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.EntityFramework
{
    public class AppDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public AppDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public AppDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>();

            _configureDbContext(options);

            return new AppDbContext(options.Options);
        }
    }
}
