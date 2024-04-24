using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace RepairLog_Server.Database
{
    public class Repaired
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
