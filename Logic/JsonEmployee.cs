using Newtonsoft.Json.Linq;
using OrgStructure.Departments;
using OrgStructure.Personnel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure
{
    class JsonEmployee
    {
        /// <summary>
        /// Возвращение работников в виде Json
        /// </summary>
        /// <param name="person">экземпляр Person</param>
        /// <returns></returns>
        public static JObject GetJObjectPerson(Person person)
        {
            JObject finished = new JObject();
            finished["Id"] = person.Id;
            finished["Name"] = person.Name;
            finished["LastNname"] = person.LastName;
            finished["Salary"] = person.Salary;
            finished["Departament"] = person.Department.Id;
            finished["Position"] = person.Position;

            //Заполняем различающиеся поля

            if (person.GetType() == typeof(Intern))
            {
                finished["Salary"] = (person as Intern).Salary;
            }

            if (person.GetType() == typeof(Employee))
            {
                finished["Salary"] = (person as Employee).Salary;
                finished["Hours"] = (person as Employee).Hours;
            }

            if (person is AbsManager)
            {
                finished["CoefSalary"] = (person as AbsManager).CoefSalary;
                finished["LowSalary"] = (person as AbsManager).LowSalary;
            }
            return finished;
        }

        /// <summary>
        /// Извлекаем работника из JToken
        /// </summary>
        /// <param name="jToken"></param>
        /// <returns></returns>
        public static Person DeserializePerson(JToken jToken)
        {
            string cls = jToken["Class"].ToString();
            int id = int.Parse(jToken["Id"].ToString());
            string name = jToken["Name"].ToString();
            string lastname = jToken["LastName"].ToString();
            string birthday = jToken["Birthday"].ToString();
            string position = jToken["Position"].ToString();
            int departmentId = int.Parse(jToken["Department"].ToString());
            switch (cls)
            {
                case "Intern":
                    return new Intern()
                    {
                        Id = id,
                        Name = name,
                        LastName = lastname,
                        Position = position,
                        DepartmentId = departmentId,
                        Salary = double.Parse(jToken["Salary"].ToString())
                    };
                case "Worker":
                    return new Employee()
                    {
                        Id = id,
                        Name = name,
                        LastName = lastname,
                        DepartmentId = departmentId,
                        Position = position,
                        Hours = int.Parse(jToken["WorkHours"].ToString()),
                        Salary = double.Parse(jToken["Salary"].ToString())
                    };
                case "LocalManager":
                    return new LocalManager()
                    {
                        Id = id,
                        Name = name,
                        LastName = lastname,
                        Position = position,
                        DepartmentId = departmentId,
                        CoefSalary = double.Parse(jToken["CoefSalary"].ToString()),
                        LowSalary = double.Parse(jToken["LowSalary"].ToString())
                    };

                case "DepBranchManager":
                    return new DepBranchManager()
                    {
                        Id = id,
                        Name = name,
                        LastName = lastname,
                        Position = position,
                        DepartmentId = departmentId,
                        CoefSalary = double.Parse(jToken["CoefSalary"].ToString()),
                        LowSalary = double.Parse(jToken["LowSalary"].ToString())
                    };
                case "BranchOfficeManager":
                    return new BranchOfficeManager()
                    {
                        Id = id,
                        Name = name,
                        LastName = lastname,
                        DepartmentId = departmentId,
                        Position = position,
                        CoefSalary = double.Parse(jToken["CoefSalary"].ToString()),
                        LowSalary = double.Parse(jToken["LowSalary"].ToString())
                    };
                case "TopManager":
                    return new TopManager()
                    {
                        Id = id,
                        Name = name,
                        LastName = lastname,
                        DepartmentId = departmentId,
                        Position = position,
                        CoefSalary = double.Parse(jToken["CoefSalary"].ToString()),
                        LowSalary = double.Parse(jToken["LowSalary"].ToString())
                    };

                default:
                    return new Intern()
                    {
                        Name = name,
                        LastName = lastname,
                        Position = position,
                        Salary = 0
                    };
            }
        }

        /// <summary>
        /// Преобразует экземпляр калсса AbsDepartament в JObject
        /// </summary>
        /// <param name="department">Сериализуемый департамент</param>
        public static JObject GetJObjectDepartament(AbsDepartment department)
        {
            JObject result = new JObject();
            result["Id"] = department.Id;
            result["inheritedId"] = department.inheritedID;
            result["Title"] = department.Title;
            //Заполняем различающиеся поля
            if (department is Department)
            {
                result["Class"] = "Department";
            }

            JArray employees = new JArray();
            foreach (var e in department.Employees)
            {
                employees.Add(GetJObjectPerson(e));
            }
            result["Employees"] = employees;
            return result;
        }

        /// <summary>
        /// извлекает Департамент с сотрудниками из JToken
        /// </summary>
        public static AbsDepartment DeserializeDepartament(JToken jToken)
        {
            int id = int.Parse(jToken["Id"].ToString());
            string title = jToken["Title"].ToString();
                       
            var d = new Department();
            d.Id = id;
            d.Title = title;
            d.Employees = new ObservableCollection<Person>();
            foreach (var e in jToken["Employees"].ToArray())
            {
              var emp = DeserializePerson(e);
              emp.Department = d;
              d.Employees.Add(emp);
            }
              return d;
              
                                
        }

        /// <summary>
        /// Сериализует департамент вместе с вложенными департаментами
        /// </summary>
        public static JObject SerializeDepartamentWithSub(AbsDepartment department)
        {
            JObject finished = new JObject();
            finished["Id"] = department.Id;
            finished["Class"] = department.GetType().Name;
            finished["InheritedID"] = department.inheritedID;
            finished["Title"] = department.Title;
                                    
          
            JArray employees = new JArray();
            foreach (var e in department.Employees)
            {
                employees.Add(GetJObjectPerson(e));
            }
            finished["Employees"] = employees;
            JArray departments = new JArray();
            if (department.Departments.Count > 0)
            {
                foreach (var d in department.Departments)
                {
                    departments.Add(SerializeDepartamentWithSub(d));
                }
            }
            finished["Departments"] = departments;
            return finished;
        }

        /// <summary>
        /// Извлекает департамент включая вложенные департаменты с сотрудниками
        /// </summary>
        public static AbsDepartment DeserealizeDepartamentWithEmp(JToken jToken)
        {
            string cls = jToken["Class"].ToString();
            int id = int.Parse(jToken["Id"].ToString());
            string title = jToken["Title"].ToString();


            var d = new Department();
            d.Id = id;
            d.Title = title;
            d.Employees = new ObservableCollection<Person>();

            foreach (var e in jToken["Employees"].ToArray())
            {
                var emp = DeserializePerson(e);
                emp.Department = d;
                d.Employees.Add(emp);
            }
            d.Departments = new ObservableCollection<AbsDepartment>();
            if (jToken["SubDepartaments"].ToArray().Length > 0)
            {
                foreach (var dep in jToken["SubDepartaments"].ToArray())
                {
                    d.Departments.Add(DeserealizeDepartamentWithEmp(dep));
                }
            }
            GetLastIds(d);
            return d;



        }


        #region Методы возвращающие последниq ID после десериализации
        /// <summary>
        /// Присваивает глобальным статическим переменным последние ID (потребуется для создания новых экземпляров)
        /// </summary>
        /// <param name="md">Департамент самого высокого уровня</param>
        static void GetLastIds(AbsDepartment md)
        {
            if (md.Id >= AbsDepartment.countID) AbsDepartment.countID = md.Id;
            var eid = BiggerEmplId(md);
            if (eid > Person.countID) Person.countID = eid;
            foreach (var i in md.Departments)
            {
                GetLastIds(i);
            }
        }
        /// <summary>
        /// Возвращает наибольший ID сотрудника депратамента
        /// </summary>
        /// <param name="dep">Департамент</param>
        /// <returns></returns>
        static int BiggerEmplId(AbsDepartment dep)
        {
            int result = 0;
            foreach (var e in dep.Employees)
            {
                if (e.Id > result) result = e.Id;
            }
            return result;
        }
        #endregion






    }
}
