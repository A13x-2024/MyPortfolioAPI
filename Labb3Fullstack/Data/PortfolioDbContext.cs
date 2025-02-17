using Labb3Fullstack.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3Fullstack.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options){ }
        public DbSet<Portfolio> Portfolios { get; set; }
    }
}
