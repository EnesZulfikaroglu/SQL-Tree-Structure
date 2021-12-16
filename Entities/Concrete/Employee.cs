using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        [ForeignKey("ParentEmployee")]
        public int? ParentId { get; set; }

        public Employee ParentEmployee { get; set; }


        public virtual List<Employee> Children { get; set; }
    }
}
