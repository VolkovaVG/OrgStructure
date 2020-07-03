using System;
using System.Collections.Generic;
using OrgStructure.Departments;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure.Personnel
{
    public abstract class AbsManager: Person
    {
        /// <summary>
        /// Отделы в управлении (задается отдельно)
        /// </summary>
        public ObservableCollection<AbsDepartment> SubordinateDepartment { get; set; }
        /// <summary>
        /// Работники подчиненного отдела
        /// </summary>
        protected ObservableCollection<Person> subordinates;
        public abstract ObservableCollection<Person> Subordinates
        {
            get;
        }

        protected double coefSalary;
        public double CoefSalary
        {
            get { return coefSalary; }
            set
            {
                if (value <= 0)
                    coefSalary = 1;
                OnPropertyChanged("");
            }
        }


        /// <summary>
        /// Минимальная ЗП
        /// </summary>
        public double LowSalary = 0;
                       
        protected string error;
        virtual public string Error
        {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChanged("");
            }
        }

        public override double SalaryPayment => ((GetAllSalaries() * CoefSalary) > LowSalary) ? 
            (GetAllSalaries() * CoefSalary) : LowSalary;

        /// <summary>
        /// Просчитывает ЗП сотрудников в подчиненных отделах
        /// </summary>
        /// <param name="start">Начальная ЗП</param>
        public abstract double GetAllSalaries();


        public AbsManager() : base() { }
        public AbsManager(string name, string surname, string position, AbsDepartment departament)
            : base(name, surname, position, departament)
        {
        }
    }
}
