using OrgStructure.Personnel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure
{
    public abstract class Person: BaseINotifyPropertyChanged
    {
        #region Поля
        public static Dictionary<string, Type> EmpTypes; //содержит все типы работников и назв дожн-ей для вывода WPF 

        public int id;
        public static int newID = 1;
        public string name;
        public string lastName;
        public string position;
        public double salary;
        public AbsDepartment department;
        int departmentId;
        //public List <string> project;
        #endregion

        #region Свойства

        public static Dictionary<string, Type> empTypes { get; set; }
        public static int countID;
        public int Id {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("");
            }
        }
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("");
            }
        }
        public double Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged("");
            }
        }
        public AbsDepartment Department
        {
            get { return department; }
            set
            {
                department = value;
                OnPropertyChanged("");
            }
        }
       
        public int DepartmentId
        {
            get { return departmentId; }
            set
            {
                departmentId = value;
                OnPropertyChanged("");
            }
        }

        #endregion


        #region Конструктор
        public Person() { }
        public Person(string name, string lastName, string position, AbsDepartment department, int id = -1) 
            : base()
        {
            if (id == -1)
                this.id = NewID();
            else this.Id = id;
            this.Name = name;
            this.LastName = lastName;
            this.Position = position;
         
            this.Department = department;
            //EmpTypes.Add("hhh", typeof(Intern))
        }
        #endregion
        /// <summary>
        /// Изменение статичного ID
        /// </summary>
        /// <returns>новый ID</returns>
        static int NewID()
        {
            newID++;
            return newID;
        }

        static Person()
        {
            int newID;
            if (EmpTypes == null)
            {
                EmpTypes = new Dictionary<string, Type>();
                EmpTypes.Add("Интерн", typeof(Intern));
                EmpTypes.Add("Сотрудник", typeof(Employee));
                EmpTypes.Add("Руководитель департамента", typeof(LocalManager)) ;
                EmpTypes.Add("Руководитель ветки департаментов", typeof(DepBranchManager));
                EmpTypes.Add("Руководитель филиала", typeof(BranchOfficeManager));
                EmpTypes.Add("Директор", typeof(TopManager));
            }
        }

        /// <summary>Статичный метод создания нового сотрудника на основе входящего типа</summary>
        /// <param name="type">Класс нужного сотрудника</param>
        public static Person CreatePerson(Type type)
        {
            Person e;
            switch (type.Name)
            {
                case "Intern":
                    e = new Intern();
                    e.id = NewID();
                    return e;
                case "Employee":
                    e = new Employee();
                    e.id = NewID();
                    return e;
                case "BranchOfficeManager":
                    e = new BranchOfficeManager();
                    e.id = NewID();
                    return e;
                case "DepBranchManager":
                    e = new DepBranchManager();
                    e.id = NewID();
                    return e;
                case "LocalManager":
                    e = new LocalManager();
                    e.id = NewID();
                    return e;
                case "TopManager":
                    e = new TopManager();
                    e.id = NewID();
                    return e;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Выплата з/п за опр период
        /// </summary>
        /// <returns></returns>
        public abstract double SalaryPayment { get; }
       
        public virtual void GiveBonus ()
        {   //Оставим для ButtonClick
            //PersonsList.ItemsSource = persons;//установка источника данных для WPF
            //var somePerson = persons[persons.IndexOf(PersonsList.SelectedItem as Person)];//выделение сотрудника для дальнейшего взаимод-я
            this.Salary += this.Salary * 0.2;
        }

        
    }
}

