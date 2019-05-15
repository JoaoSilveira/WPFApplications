using System;

namespace ContentTemplateDemo
{
    public class Employee
    {
        public string Name { get; set; }
        public string Face { get; set; }
        public DateTime BirthDate { get; set; }
        public bool LeftHanded { get; set; }

        public Employee()
        {
        }

        public Employee(string name, string face, DateTime birthDate, bool leftHanded)
        {
            Name = name;
            Face = face;
            BirthDate = birthDate;
            LeftHanded = leftHanded;
        }
    }
}
