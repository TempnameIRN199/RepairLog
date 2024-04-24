using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace RepairLog_Server.Database
{
    public class Breakdown
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Cause { get; set; }
        [Required]
        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
