using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure
{
    public class Intern: Person
    {
        public Intern() : base() { }

        public Intern(string name, string lastName, string position,  AbsDepartment departament) 
            : base(name, lastName, position,  departament) { }

        public override double SalaryPayment => Salary;

    }
}
