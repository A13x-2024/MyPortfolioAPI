
using Labb3Fullstack.Data;
using Labb3Fullstack.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3Fullstack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PortfolioDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<SkillService>();

            builder.Services.AddScoped<ProjectService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            //Skill CRUD
            app.MapPost("/skill", async (Skill skill, SkillService service) =>
            {
                await service.AddSkill(skill);
                return Results.Ok();
            });

            app.MapGet("/skills", async (SkillService service) =>
            {
                var getAll = await service.GetSkills();
                return Results.Ok(getAll);
            });

            app.MapGet("/skill/{id}", async (int id, SkillService service) =>
            {
                var skill = await service.GetSkillById(id);

                if (skill == null)
                {
                    return Results.NotFound("Project not found");
                }

                return Results.Ok(skill);
            });

            app.MapPut("/skill/{id}", async (int id, Skill skill, SkillService service) =>
            {
                var updatedSkill = await service.UpdateSkill(id, skill);
                if (updatedSkill == null)
                    return Results.NotFound("Skill not found");

                return Results.Ok(updatedSkill);
            });

            app.MapDelete("/skill/{id}", async (int id, SkillService service) =>
            {
                var isDeleted = await service.DeleteSkill(id);
                if (isDeleted == null)
                {
                    return Results.NotFound("Skill not found");
                }
                return Results.Ok(isDeleted);
            });


            //Project CRUD
            app.MapPost("/project", async (Project project, ProjectService service) =>
            {
                await service.AddProject(project);
                return Results.Ok();
            });

            app.MapGet("/projects", async (ProjectService service) =>
            {
                var projects = await service.GetProjects();
                return Results.Ok(projects);
            });

            app.MapGet("/project/{id}", async (int id, ProjectService service) =>
            {
                var project = await service.GetProjectById(id);

                if (project == null)
                {
                    return Results.NotFound("Project not found");
                }

                return Results.Ok(project);
            });

            app.MapPut("/project/{id}", async (int id, Project project, ProjectService service) =>
            {
                var updatedProject = await service.UpdateProject(id, project);
                return updatedProject != null ? Results.Ok(updatedProject) : Results.NotFound("Project not found");
            });

            app.MapDelete("/project/{id}", async (int id, ProjectService service) =>
            {
                var deletedProject = await service.DeleteProject(id);
                return deletedProject != null ? Results.Ok(deletedProject) : Results.NotFound("Project not found");
            });

            app.Run();
        }
    }
}
