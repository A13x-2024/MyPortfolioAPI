using Labb3Fullstack.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3Fullstack.Data
{
    public class ProjectService
    {
        private readonly PortfolioDbContext _db;


        public ProjectService(PortfolioDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task AddProject(Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Project>> GetProjects()
        {
            return await _db.Projects.ToListAsync();
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await _db.Projects.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Project> UpdateProject(int id, Project updatedProject)
        {
            var project = await _db.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (project == null) return null;
            project.Name = updatedProject.Name;
            project.Description = updatedProject.Description;
            await _db.SaveChangesAsync();
            return project;
        }

        public async Task<Project> DeleteProject(int id)
        {
            var deletedProject = await _db.Projects.FirstOrDefaultAsync(x => x.Id == id);
            _db.Projects.Remove(deletedProject);
            await _db.SaveChangesAsync();
            return deletedProject;
        }
    }
}
