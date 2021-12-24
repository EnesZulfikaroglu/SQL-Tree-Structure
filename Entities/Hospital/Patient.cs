using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Hospital
{
    public class Patient : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("FamilyDoctor")]
        public int FamilyDoctorId { get; set; }

        public virtual Doctor FamilyDoctor {get ;set;}
    }
}
