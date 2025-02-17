using Labb3Fullstack.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3Fullstack.Data
{
    public class PortfolioService
    {
        private readonly PortfolioDbContext _db;


        public PortfolioService(PortfolioDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task AddToPortfolio(Portfolio portfolio)
        {
            await _db.Portfolios.AddAsync(portfolio);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Portfolio>> GetPortfolios()
        {
            return await _db.Portfolios.ToListAsync();
        }

        public async Task<Portfolio> UpdatePortfolio(int id, Portfolio updatedPortfolio)
        {
            var updateportfolio = await _db.Portfolios.FirstOrDefaultAsync(x => x.Id == id);
            if (updateportfolio == null) return null;
            updateportfolio.Technology = updatedPortfolio.Technology;
            updateportfolio.YearsOfExperiance = updatedPortfolio.YearsOfExperiance;
            updateportfolio.Skillgrade = updatedPortfolio.Skillgrade;
            await _db.SaveChangesAsync();
            return updateportfolio;

        }

        public async Task<Portfolio> DeleteFromPortfolio(int id)
        {
            var deletedPortfolio = await _db.Portfolios.FirstOrDefaultAsync(x => x.Id == id);
            _db.Portfolios.Remove(deletedPortfolio);
            await _db.SaveChangesAsync();
            return deletedPortfolio;
        }
    }
}
