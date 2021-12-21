using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class EmployeeToAddDto : IDto
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
