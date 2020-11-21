using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using TrainingTask.Common.Exceptions;
using ProjectModel = TrainingTask.Common.DTO.Project;
using ProjectEF = TrainingTask.Data.EF.Model.ProjectManagement.Project;

namespace TrainingTask.Data.EF.Repository
{
    public class ProjectRepository : IRepository<ProjectModel>
    {
        private readonly TrainingTaskDbContext _context;
        private readonly IMapper _mapper;

        public ProjectRepository(TrainingTaskDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<ProjectModel> GetAllItems()
        {
            var projectsDB = _context.Projects.Include(p => p.Tasks)
                .ToList();

            var projects = projectsDB.Select(p => _mapper.Map<ProjectModel>(p));

            return projects;
        }

        public ProjectModel GetItem(int id)
        {
            var projectDb = _context.Projects.Where(e => e.Id == id)
                .Include(e => e.Tasks)
                .FirstOrDefault();

            var project = projectDb is null
                ? throw new ObjectNotFoundException(typeof(ProjectModel).ToString())
                : _mapper.Map<ProjectModel>(projectDb);

            return project;
        }

        public int AddItem(ProjectModel item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var projectDb = _mapper.Map<ProjectEF>(item);

            _context.Projects.Add(projectDb);

            _context.SaveChanges();

            return projectDb.Id;
        }

        public int RemoveItem(ProjectModel item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var projectDb = _context.Projects.Include(p => p.Tasks).FirstOrDefault(e => e.Id == item.Id) ??
                            throw new ObjectNotFoundException(typeof(ProjectEF).ToString());

            _context.Projects.Remove(projectDb);

            return _context.SaveChanges();
        }

        public int UpdateItem(ProjectModel item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var projectDb = _context.Projects.FirstOrDefault(e => e.Id == item.Id) ??
                            throw new ObjectNotFoundException(typeof(ProjectEF).ToString());

            _mapper.Map(item, projectDb);

            return _context.SaveChanges();
        }
    }
}