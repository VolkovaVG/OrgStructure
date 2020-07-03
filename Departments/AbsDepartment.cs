using Newtonsoft.Json.Linq;
using OrgStructure.Departments;
using OrgStructure.Logic;
using OrgStructure.Personnel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure
{
     public abstract class AbsDepartment : BaseINotifyPropertyChanged
     {
            static AbsDepartment add;
            public static AbsDepartment AD
            {
                get
                {
                    if (add == null)
                    {
                        add = File.Exists("Company.json") ?
                             JsonEmployee.DeserealizeDepartamentWithEmp(JToken.Parse(File.ReadAllText("Company.json"))) as Department :
                               new Recursion().Creation();
                    }
                    return add;
                }
                set
                {
                    add = value;
                }
            }
            protected string title;
            /// <summary>
            /// Наименование
            /// </summary>
            public string Title
            {
                get { return title; }
                set
                {
                    title = value;
                    OnPropertyChanged("");
                }
            }
            /// <summary>
            /// статичный никальный ID
            /// </summary>
            public static int countID;
            protected int id;
            /// <summary>
            /// Уникальный ID
            /// </summary>
            public int Id
            {
                get => id;
                set
                {
                    id = value;
                    OnPropertyChanged("");
                }
            }
            public int inheritedID;
            /// <summary>
            /// Наследуемый департамент, ID
            /// </summary>
            public int InheritedID
            {
                get { return inheritedID; }
                set
                {
                    inheritedID = value;
                    OnPropertyChanged("");
                }
            }


             /// <summary>
             /// Сотрудники департамента
             /// </summary>
            public ObservableCollection<Person> Employees { get; set; }

             /// <summary>
        /// Коллекция подчиненных департаментов
        /// </summary>
             public ObservableCollection<AbsDepartment> Departments { get; set; }

               static AbsDepartment()
               {
                 countID = 1;
               }
               public AbsDepartment() { }
               /// <summary>
        /// Конструктор абст.деп-та
        /// </summary>
        /// <param name="title">название</param>
        /// <param name="inheritedID">ID</param>
               public AbsDepartment(string title, int inheritedID, int id = -1)
               {
                Id = (id == -1) ? NextID() : id;

                 Title = title;
                 InheritedID = inheritedID;
                 Employees = new ObservableCollection<Person>();
                 Departments = new ObservableCollection<AbsDepartment>();
               }

                static int NextID()
                {
                  countID++;
                  return countID;
                }
        /// <summary>
        /// массив по должностям работников
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetCountEmployees()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("Intern", 0);
            dic.Add("Employee", 0);
            dic.Add("LocalManager", 0);
            dic.Add("DepBranchManager", 0);
            dic.Add("BranchOfficeManager", 0);
            dic.Add("TopManager", 0);
           
            foreach (var w in Employees)
            {
                switch (w)
                {
                    case TopManager _:
                        dic["TopManager"]++;
                        break;

                    case DepBranchManager _:
                        dic["DepBranchManager"]++;
                        break;
                    case BranchOfficeManager _:
                        dic["BranchOfficeManager"]++;
                        break;
                    case Employee _:
                        dic["Employee"]++;
                        break;
                    case Intern _:
                        dic["Intern"]++;
                        break;
                    case LocalManager _:
                        dic["LocalManager"]++;
                        break;
                   
                    
                }
            }
            return dic;
        }

        #region Добавить/Удалить департамент
        /// <summary>Редактирование департамента</summary>
        /// <param name="editedDepartment">Отредактированный экземпляр депратамента</param>
        public abstract void Edit(AbsDepartment editedDepartment);

        /// <summary>Добавить подчиненный департамент</summary>
        public void AddDepartment(AbsDepartment dep)
        {
            Departments.Add( new Department(dep.title, Id));
        }

        /// <summary> Удалить текущий департамент из структуры организации </summary>
        public void Remove()
        {
            remove(AD, this.Id);
        }
        void remove(AbsDepartment dep, int id)
        {
            foreach (var d in dep.Departments)
            {
                if (d.id == id)
                {
                    dep.Departments.Remove(d);
                    return;
                }
                else remove(d, id);
            }
        }
        #endregion

        #region Работа с сотрудниками
        public void AddEmployee(Person employee)
        {
            if (employee == null) return;
            employee.Department = this;
            Employees.Add(employee);
        }
        public void RemoveEmployee(Person employee)
        {
            Employees.Remove(employee);
        }
        #endregion

    }
}
