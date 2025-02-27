using ITI_project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITI_project.ModelView
{
    public class ListOfDeptWithObjCourse
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int Degree { set; get; }
        public int MinDegree { set; get; }
        public int Hours { set; get; }
        public int DeptID { set; get; }

        public List<Department> departments { set; get; }
    }
}
