using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Hospital
{
    public class Doctor : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Patient> Patients { get; set; }

    }
}
