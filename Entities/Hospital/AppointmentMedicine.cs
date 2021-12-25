using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Hospital
{
    public class AppointmentMedicine : IEntity
    {
        public int Id { get; set; }

        [ForeignKey("appointment")]
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }

        [ForeignKey("medicine")]
        public int MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}
