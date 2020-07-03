using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure.Personnel
{
    /// <summary>
    /// Здесь список всех сотрудников
    /// </summary>
    class AllEmployees
    {
        static ObservableCollection<Person> EmployeesList;

        public Person this[int id]
        {
            get
            {
                Person pers = null;
                foreach (var person in EmployeesList)
                {
                    if (person.Id == id) pers = person;
                    break;
                }
                return pers;
            }
        }
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="person">Экземпляр сотрудника</param>
        public void AddEmployee(Person person)
        {
            EmployeesList.Add(person);
        }
        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="person">Экземпляр сотрудника</param>
        public void RemoveEmployee(Person person)
        {
            EmployeesList.Remove(person);
        }
        /// <summary>
        /// Получить всех сотрудников департамента и вложенных департаментов)
        /// </summary>
        /// <param name="departament">Экземпляр департамента</param>
        /// <returns></returns>
        static public ObservableCollection<Person> PersonalsOfDepartament(AbsDepartment department)
        {
            ObservableCollection<Person> workers = new ObservableCollection<Person>();
            foreach (Person w in EmployeesList)
            {
                if (w.DepartmentId == department.Id) workers.Add(w);
            }
            foreach (var d in department.Departments)
            {
                workers.Concat(PersonalsOfDepartament(d));
            }
            return workers;
        }
        static public ObservableCollection<Person> WorkersOfDepartament(AbsDepartment department)
        {
            ObservableCollection<Person> employees = new ObservableCollection<Person>();
            foreach (Person w in EmployeesList)
            {
                if (w.DepartmentId == department.Id && w is Intern) employees.Add(w);
                if (w.DepartmentId == department.Id && w is Employee) employees.Add(w);
            }
            foreach (var d in department.Departments)
            {
                employees.Concat(WorkersOfDepartament(d));
            }
            return employees;
        }
        static public ObservableCollection<Person> HeadsOfDepartament(AbsDepartment department)
        {
            ObservableCollection<Person> employees = new ObservableCollection<Person>();
            foreach (Person w in EmployeesList)
            {
                if (w.DepartmentId == department.Id && w is LocalManager) employees.Add(w);
            }
            foreach (var d in department.Departments)
            {
                employees.Concat(HeadsOfDepartament(d));
            }
            return employees;
        }

    }
}
