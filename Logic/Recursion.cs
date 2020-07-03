using System;
using OrgStructure.Departments;
using OrgStructure.Personnel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace OrgStructure.Logic
{
    class Recursion
    {
        #region Генерация данных
        public Department Creation()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            var Dep = new Department("SpaceX-2");
            Dep.Departments = new ObservableCollection<AbsDepartment>();
            for (int i = 0; i < random.Next(1, 7); i++)
            {
                Dep.Departments.Add(GenerateDepartament(Dep.Id, random.Next(0, 5), random, random.Next(5, 10)));
            }
            Dep.Employees = Second_Level_Employees(random.Next(10, 20), Dep);
            return Dep;
        }

        /// <summary>
        /// Генерация структуры департаментов
        /// </summary>
        /// <param name="inheritedId">ID родительского департамента</param>
        /// <param name="countDeps">Количство вложенных департаментов</param>
        /// <param name="rnd">Рандомная переменная, для рандомного генерирования</param>
        /// <param name="iterations">Количество итераций всего цикла генерации</param>
        /// <returns>Департамент</returns>
        Department GenerateDepartament(int inheritedId, int countDeps, Random rnd, int iterations)
        {
            iterations--;
            Department dep = new Department("", inheritedId);
            dep.Title = $"Департамент № {dep.Id}";
            dep.Departments = new ObservableCollection<AbsDepartment>();
            if (iterations > 0)
            {
                for (int i = 0; i < countDeps; i++)
                {
                    dep.Departments.Add(GenerateDepartament(dep.Id, rnd.Next(0, 5), rnd, iterations));
                }

            }
            return dep;
        }

        /// <summary>
        /// Созадает младший состав работников отдела (Вспомогательный метод)
        /// </summary>
        /// <param name="count">Количество работников</param>
        /// <param name="dep">Экземпляр отдела</param>
        /// <returns>Коллекцию работников</returns>
        ObservableCollection<Person> First_Level_Employees(int count, AbsDepartment dep)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            ObservableCollection<Person> employees = new ObservableCollection<Person>();
            int interns = random.Next(0, count / 3);
            int empl = count - interns;
            int hw = (count / random.Next(4, 9)) + 1;
            for (int i = 0; i < interns; i++)
            {
                Intern inter = new Intern($"Интерн_{i}", $"Отдела_{dep.Id}", "Интерн",  dep);
                inter.Salary = 25000;
                employees.Add(inter);
            }
            for (int i = 0; i < empl; i++)
            {
                Employee emp = new Employee($"Рабочий{i}", $"Отдела_{dep.Id}", "Сотрудник", dep);
                emp.Salary = 150;
                emp.Hours = random.Next(0, 200);
                employees.Add(emp);
            }
            for (int i = 0; i < hw; i++)
            {
                LocalManager wkr = new LocalManager($"Главарь{i}", $"Отдела_{dep.Id}", "Руководитель отдела", dep);
                wkr.CoefSalary = 0.15;
                wkr.LowSalary = 1000;
                employees.Add(wkr);
            }
            return employees;
        }

        /// <summary>
        /// Создает работников отдела
        /// </summary>
        /// <param name="count">Количество работников</param>
        /// <param name="dep">Экземпляр департамента</param>
        /// <returns>Коллекцию работников</returns>
        ObservableCollection<Person> Second_Level_Employees(int count, AbsDepartment dep)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            ObservableCollection<Person> employees = new ObservableCollection<Person>();
            if (dep.Departments.Count == 0)
            {
                employees = First_Level_Employees(count, dep);
            }
            else
            {
                int ld = 0;
                foreach (var d in dep.Departments)
                {
                    if (d.Employees.Count == 0)
                    {
                        d.Employees = Second_Level_Employees(count, d);
                    }
                    foreach (var e in d.Employees)
                    {
                        if (e.GetType() == typeof(DepBranchManager)) ld++;
                        if (e.GetType() == typeof(BranchOfficeManager)) ld++;

                    }
                }

                if (ld > 0)
                {
                    employees = First_Level_Employees(count, dep);
                    BranchOfficeManager emp = new BranchOfficeManager($"Еще больший Начальник", $"Отдела_{dep.Id}", "Директор филиала", dep);
                    emp.CoefSalary = 0.4;
                    emp.LowSalary = 6000;
                    employees.Add(emp);
                }
                else
                {
                    employees = First_Level_Employees(count, dep);
                    DepBranchManager emp = new DepBranchManager($"Большой начальник", $"Отдела_{dep.Id}", "Директор ветки департаментов", dep);

                    emp.CoefSalary = 0.25;
                    emp.LowSalary = 2000;
                    employees.Add(emp);
                }
                //Заполняем верхний уровень департаментов
                if (dep.inheritedID == 1)
                {
                    //Обычный персонал. Секретарши и т.п.
                    employees = First_Level_Employees(random.Next(3, 6), dep);
                    for (int i = 0; i < dep.Departments.Count; i++)
                    {
                        TopManager wkr = new TopManager($"ТОП", $"Департамента_{dep.Id}", "Гендир", dep);

                        wkr.CoefSalary = 0.4;
                        wkr.LowSalary = 12000;
                        employees.Add(wkr);
                    }
                }
                
            }
            return employees;
        }
        #endregion

        




    }
}
