using OrgStructure.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OrgStructure.WPF
{
    /// <summary>
    /// Логика взаимодействия для _1_Department.xaml
    /// </summary>
    public partial class _1_Department : Window, INotifyPropertyChanged
    {
        public AbsDepartment ReturnDepartament { get; set; }
        private string titl;
        

        #region Поля департамента
        public string Titl
        {
            get => titl;
            set
            {
                titl = value;
                OnPropertyChanged();
            }
        }
     
        
        #endregion
        public _1_Department()
        {
            InitializeComponent();
        }
        public _1_Department(AbsDepartment dep) : this()
        {
            InitializeControls(dep);
        }

        void InitializeControls(AbsDepartment dep)
        {
            TitleTB.Text = dep.Title;
            ReturnDepartament = new Department();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ReturnDepartament == null) ReturnDepartament = new Department();
            ReturnDepartament.Title = TitleTB.Text;
            
            this.DialogResult = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}

