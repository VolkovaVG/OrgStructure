using OrgStructure.Personnel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure
{
    class LocalManager: AbsManager

    {   /// <summary>
        /// Подчиненный персонал
        /// </summary>
        public override ObservableCollection<Person> Subordinates
        {
            get
            {
                ObservableCollection<Person> sub = new ObservableCollection<Person>();
                foreach (var d in SubordinateDepartment)
                {
                    sub.Concat(AllEmployees.PersonalsOfDepartament(d));
                }
                return sub;
            }
        }

        public override string Error
        {
            get { return error; }
            set
            {
                if (SubordinateDepartment.Count > 1)
                    error = "В управлении более одного отдела, не жадничать";
                if (SubordinateDepartment.Count < 1)
                    error = "В управлении отсутствуют отделы, не лениться";
                OnPropertyChanged("");
            }
        }


        public LocalManager() { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="position"></param>
        /// <param name="department"></param>
        public LocalManager(string name, string surname, string position, AbsDepartment department) 
            : base(name, surname, position, department)
        {
            error = "";
        }


        /// <summary>
        /// Расчет зп для местного короля
        /// </summary>
        /// <returns></returns>
        public override double GetAllSalaries()
        {
            double sal = 0;
            foreach (var w in Subordinates)
            {
                if (w is Intern)
                    sal += w.Salary * CoefSalary;
                if (w is Employee)
                    sal += w.Salary * CoefSalary;
            }
            if (sal <= LowSalary && LowSalary != 0) sal = LowSalary;
            return sal;
        }
       


    }
}
