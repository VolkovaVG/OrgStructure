using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для _4_AllSubordinates.xaml
    /// </summary>
    public partial class _4_AllSubordinates : Window
    {
        Dictionary<string, Type> empTypes => Person.empTypes;

        public _4_AllSubordinates()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ClassCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var SelectedType = Classes[ClassCB.SelectedValue.ToString()];
            //switch (SelectedType.Name)
            switch (empTypes[ClassCB.SelectedValue.ToString()].Name)
            {
                case "Intern":
                    _2_Intern i = new _2_Intern();
                    i.DataContext = DataContext;
                    contentPresenter.Content = i;
                    break;
                case "Employee":
                    _3_Employee wc = new _3_Employee();
                    wc.DataContext = DataContext;
                    contentPresenter.Content = wc;
                    break;
                default:
                    _5_Manager mn = new _5_Manager();
                    mn.DataContext = DataContext;
                    contentPresenter.Content = mn;
                    break;
            }
        }
    }
}
