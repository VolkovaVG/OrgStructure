using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgStructure.Departments
{
    public class Department: AbsDepartment
    {
        public Department()
            : base() { }
        public Department(string title, int inheritedId = 0) 
            : base(title, inheritedId) { }

        public override void Edit(AbsDepartment editedDepartment)
        {
            if (editedDepartment.GetType() == this.GetType()) Title = editedDepartment.Title;
        }
    }
}
