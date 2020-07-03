using OrgStructure.Personnel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure.WPF
{
    class AllSubMenu: IAllSubordinates
    {
        private Person employee;
        private string selectedClassKey;
        private double salary;
        private double coefSalary;
        private int workHours;
        private double lowSalary;
        private bool isNewEmployee;

        /// <summary>
        /// Смотрим или редактируем сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        public AllSubMenu(Person employee) : base()
        {
            Employee = employee;
            isNewEmployee = false;
            ChangeSelectedClass(employee.GetType());
        }

        public AllSubMenu()
        {
            isNewEmployee = true;
        }

        public IEnumerable<string> EmployeeClasses => Person.empTypes.Keys;
        public Person Employee
        {
            get => employee;
            set
            {
                employee = value;
                OnPropertyChanged();
            }
        }

        public string SelectedClassKey
        {
            get => selectedClassKey;
            set
            {
                selectedClassKey = value;
                if (IsNewEmployee)
                    Employee = Person.CreatePerson(GetEmployeeType(value));
                OnPropertyChanged();
            }
        }

        private void ChangeSelectedClass(Type type)
        {
            SelectedClassKey = Person.empTypes.First(o => o.Value == type).Key;
        }

        public bool IsNewEmployee
        {
            get => isNewEmployee;
            set
            {
                isNewEmployee = value;
                OnPropertyChanged();
            }
        }

        /// <summary>Возвращает выбранный тип из коллекции.</summary>
        private Type GetEmployeeType(string value) =>Person.empTypes[value];

        public double Salary
        {
            get
            {
                if (Employee is Intern emp) return emp.Salary;
                if (Employee is Employee empl) return empl.Salary;
                else return salary;
            }
            set
            {
                if (Employee is Intern emp) emp.Salary = value;
                if (Employee is Employee empl) empl.Salary = value;
                else salary = value;
                OnPropertyChanged();
            }
        }
        public int WorkHours
        {
            get
            {
                if (Employee is Employee emp) return emp.Hours;
                else return workHours;
            }
            set
            {
                if (Employee is Employee emp) emp.Hours = value;
                else workHours = value;
                OnPropertyChanged();
            }
        }
        public double CoefSalary
        {
            get
            {
                if (Employee is AbsManager emp) return emp.CoefSalary;
                else return coefSalary;
            }
            set
            {
                if (Employee is AbsManager emp) emp.CoefSalary = value;
                else coefSalary = value;
                OnPropertyChanged();
            }
        }
        public double LowSalary
        {
            get
            {
                if (Employee is AbsManager emp) return emp.LowSalary;
                else return lowSalary;
            }
            set
            {
                if (Employee is AbsManager emp) emp.LowSalary = value;
                else lowSalary = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
