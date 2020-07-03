using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure.Personnel
{
    class Employee : Person
    {
        #region Поля и свойства
        protected new double salary;
        public new double Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged("");
            }
        }
        int hours;
        public int Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                OnPropertyChanged("");
            }
        }
        #endregion


        /// <summary>
        /// Выплаты почасовые
        /// </summary>
        /// <returns></returns>
        public double Payment()
        {
            return Hours * Salary;
        }
        /// <summary>
        /// передаем в единое поле Person
        /// </summary>
        public override double SalaryPayment => Payment();


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="position"></param>
        /// <param name="departament"></param>
        public Employee(string name, string lastName, string position,  AbsDepartment departament)
            : base(name, lastName, position, departament) { }

        public Employee() : base() { }
       
    }
}
