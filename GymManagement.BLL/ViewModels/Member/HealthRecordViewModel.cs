using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Member
{
    public class HealthRecordViewModel
    {
        public int Id { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }
    }
}
