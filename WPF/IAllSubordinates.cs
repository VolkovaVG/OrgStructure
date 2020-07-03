using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure.WPF
{
    public interface IAllSubordinates
    {
        /// <summary>Список описаний типов</summary>
        IEnumerable<string> EmployeeClasses { get; }
        /// <summary>Экземпляр класса работника</summary>
        Person Employee { get; set; }
        /// <summary>Выбранный тип в окне</summary>
        string SelectedClassKey { get; set; }
        /// <summary>Зарплата для BaseSubordinates</summary>
        double Salary { get; set; }
        /// <summary>Отработанные часы для Worker</summary>
        int WorkHours { get; set; }
        /// <summary>Коэфициент ЗП для BaseDirector</summary>
        double CoefSalary { get; set; }
        /// <summary>Минимальная ЗП для BaseDirector</summary>
        double LowSalary { get; set; }
        /// <summary>Можно ли выбирать класс из выпадающего списка</summary>
        bool IsNewEmployee { get; set; }
    }
}
