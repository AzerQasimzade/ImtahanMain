using AmoebaProject.Models;

namespace AmoebaProject.Areas.ViewModels
{
    public class PaginationVM
    {
        public int CurrentPage { get; set; }
        public double TotalPage { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
