using OrgStructure.Personnel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure
{
    class TopManager: BranchOfficeManager
    {
        public TopManager()
        {
        }

        public TopManager(string name, string surname, string position, AbsDepartment departament) 
            : base(name, surname, position, departament)
        {
        }

        /// <summary>
        /// Просчитывает ЗП товарищей трудящихся
        /// </summary>
        /// <param name="start">Начальная ЗП</param>
        /// <param name="dep">Департамент для подсчета</param>
        protected double GetAllSalaries(AbsDepartment dep, double start)
        {
            double sal = start;
            foreach (var e in dep.Employees)
            {
                if (e is Intern)
                    sal += e.Salary * CoefSalary;
                if (e is Employee)
                    sal += e.Salary * CoefSalary;
            }
            foreach (var d in dep.Departments)
            {
                sal = GetAllSalaries(d, sal);
            }
            return sal;
        }
    }
}
