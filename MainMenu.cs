using OrgStructure.Logic;
using OrgStructure.WPF;
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
    public class MainMenu : BaseINotifyPropertyChanged
    {
        public ObservableCollection<AbsDepartment> Deps { get; set; }

        public AbsDepartment SelectedDepartament { get; set; }
        public Person SelectedEmployee { get; set; }

        public MainMenu()
        {
            Deps = new ObservableCollection<AbsDepartment>() { AbsDepartment.AD };
        }

        #region Меню Департаент
        RelayCommand eDepartament;
        /// <summary>
        /// Редактировать выделенный депатамент
        /// </summary>
        public RelayCommand EDepartament
        {
            get
            {
                return eDepartament ??
                  (eDepartament = new RelayCommand(o =>
                  {
                      _1_Department ed = new _1_Department(o as AbsDepartment);
                      if (ed.ShowDialog() == true)
                          (o as AbsDepartment).Edit(ed.ReturnDepartament);
                  }, _ => SelectedDepartament != null));
            }
        }

        RelayCommand addDepartament;
        /// <summary>
        /// Редактировать выделенный депатамент
        /// </summary>
        public RelayCommand AddDepartament
        {
            get
            {
                return addDepartament ??
                  (addDepartament = new RelayCommand(o =>
                  {
                      _1_Department ed = new _1_Department();
                      if (ed.ShowDialog() == true)
                          (o as AbsDepartment).AddDepartment(ed.ReturnDepartament);
                  }, _ => SelectedDepartament != null));
            }
        }


        RelayCommand removeDepartament;
        /// <summary>Удалить выделенный депатамент</summary>
        public RelayCommand RemoveDepartament => removeDepartament ??
                  (removeDepartament = new RelayCommand(o => SelectedDepartament.Remove(),
                   _ => SelectedDepartament != null));
        #endregion

        #region Меню Сотрудники

        RelayCommand delEmployee;
        /// <summary>Увольнение сотрудника</summary>
        public RelayCommand DelEmployee
        {
            get
            {
                return delEmployee ??
                  (delEmployee = new RelayCommand(o =>
                  {
                      (o as AbsDepartment).RemoveEmployee(SelectedEmployee);
                  }, _ => SelectedEmployee != null));
            }
        }


        //RelayCommand editEmployee;
        ///// <summary>Редактирование сотрудника</summary>
        //public RelayCommand EditEmployee
        //{
        //    get
        //    {
        //        return editEmployee ??
        //          (editEmployee = new RelayCommand(o =>
        //          {
        //              AllSubMenu vm = new AllSubMenu(SelectedEmployee.Clone());
        //              EmployeeDialogView ed = new EmployeeDialogView();
        //              ed.DataContext = vm;
        //              ed.Title = "Редактирование сотрудника";
        //              ed.ShowDialog();
        //              if (ed.DialogResult == true)
        //              {
        //                  SelectedEmployee.CopyFrom(vm.Employee);
        //              }
        //          }, _ => SelectedEmployee != null));
        //    }
        //}

        RelayCommand addEmployee;
        /// <summary>Новый сотрудник</summary>
        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??
                  (addEmployee = new RelayCommand(o =>
                  {
                      AbsDepartment dep = o as AbsDepartment;
                      if (dep != null)
                      {
                         AllSubMenu asm = new AllSubMenu();
                          _4_AllSubordinates ed = new _4_AllSubordinates();
                          ed.DataContext = asm;
                          ed.Title = "Новый сотрудник";
                          ed.ShowDialog();
                          if (ed.DialogResult == true)
                          {
                              dep.AddEmployee(asm.Employee);
                          }
                      }
                  }));
            }
        }
        #endregion

        #region Меню Файл
        RelayCommand generate;
        /// <summary>Генерировать новую структуру</summary>
        public RelayCommand Generate => generate ?? (generate = new RelayCommand(_ => Deps[0] = new Recursion().Creation()));
        #endregion


    }
}
