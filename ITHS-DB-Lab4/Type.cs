using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITHS_DB_Lab4
{
    class Type
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
