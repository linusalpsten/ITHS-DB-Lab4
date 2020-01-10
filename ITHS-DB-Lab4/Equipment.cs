using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITHS_DB_Lab4
{
    class Equipment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<SessionEquipment> SessionEquipment { get; set; }
    }
}
