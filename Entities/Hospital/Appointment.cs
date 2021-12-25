using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Hospital
{
    public class Appointment : IEntity
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public bool IsApproved { get; set; }

        //public bool IsFamilyDoctor { get; set; }

        public virtual List<Medicine> Medicines { get; set; }
    }
}
