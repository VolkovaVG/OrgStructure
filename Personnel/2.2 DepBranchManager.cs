using OrgStructure.Personnel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure
{
    class DepBranchManager: LocalManager
    {
        public DepBranchManager()
        {
        }
        public DepBranchManager(string name, string surname, string position, AbsDepartment department) 
            : base(name, surname, position, department)
        { }

        public override double GetAllSalaries()
        { 
            double sal = 0;
            foreach (var e in Department.Employees)
            {
                if (Department.Departments.Count == 0)
                    if (e is LocalManager) sal += e.SalaryPayment;
                    else
                    if (e.GetType() == typeof(DepBranchManager)) sal += e.SalaryPayment;
            }
            foreach (var d in Department.Departments)
            {
                sal += d.Employees.OfType<LocalManager>().Sum(g => g.SalaryPayment);
            }

            return sal;
        }
          

    }
}
