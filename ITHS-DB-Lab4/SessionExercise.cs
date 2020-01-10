using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITHS_DB_Lab4
{
    class SessionExercise
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        [Range(1, 10)]
        public int Toughness { get; set; } = 5;
    }
}
