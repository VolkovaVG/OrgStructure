using OrgStructure.WPF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OrgStructure
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainMenu mm;
        _0_CompanyInfo ci;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            mm = new MainMenu();

            ci = new _0_CompanyInfo();
            ci.DataContext = mm;
            ci.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            File.WriteAllText("DB.json", JsonEmployee.SerializeDepartamentWithSub(mm.Deps[0]).ToString());
        }
    }
}
