using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Hospital
{
    class PatientDoctor : IEntity
    {
        public int Id { get; set; }

        [ForeignKey("patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [ForeignKey("doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public DateTime? ExpireDate { get; set; }
    }
}
