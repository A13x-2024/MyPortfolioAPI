using Labb3Fullstack.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3Fullstack.Data
{
    public class SkillService
    {
        private readonly PortfolioDbContext _db;


        public SkillService(PortfolioDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task AddSkill(Skill skill)
        {
            await _db.Skills.AddAsync(skill);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Skill>> GetSkills()
        {
            return await _db.Skills.ToListAsync();
        }

        public async Task<Skill> GetSkillById(int id)
        {
            return await _db.Skills.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Skill> UpdateSkill(int id, Skill updatedSkill)
        {
            var skill = await _db.Skills.FirstOrDefaultAsync(x => x.Id == id);
            if (skill == null) return null;
            skill.Name = updatedSkill.Name;
            skill.YearsOfExperience = updatedSkill.YearsOfExperience;
            skill.SkillLevel = updatedSkill.SkillLevel;
            await _db.SaveChangesAsync();
            return skill;
        }

        public async Task<Skill> DeleteSkill(int id)
        {
            var deletedSkill = await _db.Skills.FirstOrDefaultAsync(x => x.Id == id);
            _db.Skills.Remove(deletedSkill);
            await _db.SaveChangesAsync();
            return deletedSkill;
        }
    }
}
