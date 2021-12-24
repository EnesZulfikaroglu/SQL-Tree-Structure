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

        public DateTime date { get; set; }

        [ForeignKey("doctor")]
        public int doctorId { get; set; }
        public virtual Doctor doctor { get; set; }

        [ForeignKey("patient")]
        public int patientId { get; set; }
        public virtual Patient patient { get; set; }

        public bool IsApproved { get; set; }

        //public bool IsFamilyDoctor { get; set; }

        public virtual List<Medicine> Medicines { get; set; }
    }
}
