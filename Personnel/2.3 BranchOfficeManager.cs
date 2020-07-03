using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure
{
    class BranchOfficeManager: LocalManager
    {
        public BranchOfficeManager()
        {
        }

        public BranchOfficeManager(string name, string surname, string position, AbsDepartment departament)
            : base(name, surname, position, departament)
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
                sal += (d.Departments.Count == 0) ?
                        d.Employees.OfType<LocalManager>().Sum(g => g.SalaryPayment) :
                            (d.GetCountEmployees()["BranchOfficeManager"] > 0) ?
                            d.Employees.OfType<BranchOfficeManager>().Sum(g => g.SalaryPayment) :
                            d.Employees.OfType<DepBranchManager>().Sum(g => g.SalaryPayment);
            }
            return sal;
        }

    }
}
