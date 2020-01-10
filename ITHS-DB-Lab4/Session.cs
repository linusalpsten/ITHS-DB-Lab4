using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITHS_DB_Lab4
{
    class Session
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public List<SessionEquipment> SessionEquipment { get; set; }
        public List<SessionExercise> SessionExercises { get; set; }
    }
}
