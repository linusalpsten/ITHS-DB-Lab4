using System;
using System.Collections.Generic;
using System.Text;

namespace ITHS_DB_Lab4
{
    class SessionEquipment
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
    }
}
