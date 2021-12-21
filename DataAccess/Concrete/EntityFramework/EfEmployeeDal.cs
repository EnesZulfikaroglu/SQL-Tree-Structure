using AutoMapper;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.EntityFramework.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployeeDal : GenericRepositoryBase<Employee>, IEmployeeDal
    {
        public EmployeeTreeContext _context { get; set; }
        private IMapper _mapper;
        public EfEmployeeDal(EmployeeTreeContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public Employee SafeDelete(Employee employee)
        {
            // Get the Data 
            var children = _context.Employees.Where(x => x.ParentId == employee.Id).ToList();
            var oldParent = _context.Set<Employee>().SingleOrDefault(x => x.Id == employee.Id);


            foreach (var child in children)
            {
                // Update the ParentId of the children
                child.ParentId = oldParent.ParentId;
                Update(child);
            }

            // Delete the old parent
            Delete(oldParent);
            return (oldParent);
            /*
            var deletedEntity = _context.Entry(employee);
            deletedEntity.State = EntityState.Deleted;
            _context.SaveChanges();
            */
        }

        public IList<Employee> GetTree()
        {
            var Root = _context.Employees.Where(x => x.ParentId == null).ToList();

            foreach (var parent in Root)
            {
                TreeHelper(parent);
            }

            return Root.ToList();
        }

        public void TreeHelper(Employee parent = null)
        {
            parent.Children = _context.Employees.Where(x => x.ParentId == parent.Id).ToList();

            if (parent.Children.Count != 0)
            {
                foreach (var child in parent.Children)
                {
                    TreeHelper(child);
                }
            }
            else
            {
                parent.Children = null;
            }

        }

    }
}

