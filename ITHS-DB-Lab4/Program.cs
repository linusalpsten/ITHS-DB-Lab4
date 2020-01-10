using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITHS_DB_Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            PopulateDatabase();
            ReadFromDatabase();
        }

        static void ReadFromDatabase()
        {
            //Hämta data
            using (Context context = new Context())
            {
                Console.WriteLine("Användare och deras träningspass");
                foreach (User user in context.Users.Include(u => u.Sessions).ToList())
                {
                    Console.WriteLine(user.FirstName + " " + user.LastName);
                    foreach (Session session in user.Sessions)
                    {
                        Console.WriteLine("-" + session.Name);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("Oanvänd utrustning");
                List<Equipment> unusedEquipment = context.Equipment.Include(e => e.SessionEquipment).Where(e => e.SessionEquipment.Count == 0).ToList();
                foreach (Equipment ue in unusedEquipment)
                {
                    Console.WriteLine(ue.Name);
                }
                Console.WriteLine();

                Console.WriteLine("Hur många gånger utrustning har änvänts");
                List<Equipment> usedEquipment = context.Equipment.Include(e => e.SessionEquipment).Where(e => e.SessionEquipment.Count > 0).ToList();
                foreach (Equipment ue in usedEquipment)
                {
                    Console.WriteLine(ue.Name + ": " + ue.SessionEquipment.Count);
                }
                Console.WriteLine();

                Console.WriteLine("Jobbigaste övning/övningar");
                List<SessionExercise> toughSessionExercises = context.SessionExercises.Include(se => se.Exercise).Include(se => se.Session).ToList().GroupBy(se => se.Toughness).OrderBy(se => se.Key).Last().ToList();
                foreach (SessionExercise sessionExercise in toughSessionExercises)
                {
                    Console.WriteLine("Träningspass: " + sessionExercise.Session.Name);
                    Console.WriteLine("Övning: " + sessionExercise.Exercise.Name);
                    Console.WriteLine("Jobbighet: " + sessionExercise.Toughness);
                    Console.WriteLine("-");
                }
            }
        }

        static void PopulateDatabase()
        {
            //Lägg till data

            using (Context context = new Context())
            {
                context.Users.Add(new User() { FirstName = "Kalle", LastName = "Anka" });
                context.Users.Add(new User() { FirstName = "Musse", LastName = "Pigg" });
                context.Users.Add(new User() { FirstName = "Joakim", LastName = "Anka" });

                context.Types.Add(new Type() { Name = "Löpning" });
                context.Types.Add(new Type() { Name = "Cykling" });
                context.Types.Add(new Type() { Name = "Simning" });
                context.Types.Add(new Type() { Name = "Blandat" });

                context.Exercises.Add(new Exercise() { Name = "100m Simning", Description = "Simma 100 meter på valfritt sätt" });
                context.Exercises.Add(new Exercise() { Name = "200m Simning", Description = "Simma 200 meter på valfritt sätt" });
                context.Exercises.Add(new Exercise() { Name = "500m Löpning", Description = "Spring 500 meter på valfritt sätt" });
                context.Exercises.Add(new Exercise() { Name = "1km Löpning", Description = "Spring 1 kilometer på valfritt sätt" });
                context.Exercises.Add(new Exercise() { Name = "1km Cykling", Description = "Cykla 1 kilometer på valfritt sätt" });
                context.Exercises.Add(new Exercise() { Name = "2km Cykling", Description = "Cykla 2 kilometer på valfritt sätt" });

                context.SaveChanges();

                Type blandat = context.Types.First(t => t.Name == "Blandat");
                int KalleAnkaId = context.Users.First(u => u.FirstName == "Kalle" && u.LastName == "Anka").Id;
                int MussePiggId = context.Users.First(u => u.FirstName == "Musse" && u.LastName == "Pigg").Id;
                int JoakimId = context.Users.First(u => u.FirstName == "Joakim" && u.LastName == "Anka").Id;
                context.Sessions.Add(new Session() { Name = "Pass1", TypeId = blandat.Id, UserId = KalleAnkaId });
                context.Sessions.Add(new Session() { Name = "Pass2", TypeId = blandat.Id, UserId = KalleAnkaId });
                context.Sessions.Add(new Session() { Name = "Pass3", TypeId = blandat.Id, UserId = KalleAnkaId });
                context.Sessions.Add(new Session() { Name = "Pass4", TypeId = blandat.Id, UserId = KalleAnkaId });
                context.Sessions.Add(new Session() { Name = "Pass5", TypeId = blandat.Id, UserId = KalleAnkaId });
                context.Sessions.Add(new Session() { Name = "Pass6", TypeId = blandat.Id, UserId = KalleAnkaId });
                context.Sessions.Add(new Session() { Name = "Lite blandat", TypeId = blandat.Id, UserId = KalleAnkaId });
                context.Sessions.Add(new Session()
                {
                    Name = "Löpning",
                    Description = "Löpa",
                    UserId = KalleAnkaId,
                    TypeId = context.Types.First(t => t.Name == "Löpning").Id
                });
                context.Sessions.Add(new Session()
                {
                    Name = "Cykling",
                    Description = "Cykla",
                    UserId = MussePiggId,
                    TypeId = context.Types.First(t => t.Name == "Cykling").Id
                });
                context.Sessions.Add(new Session()
                {
                    Name = "Simning",
                    Description = "Simma",
                    UserId = JoakimId,
                    TypeId = context.Types.First(t => t.Name == "Simning").Id
                });

                context.SaveChanges();

                context.SessionExercises.Add(new SessionExercise()
                {
                    SessionId = context.Sessions.First(s => s.Name == "Löpning").Id,
                    ExerciseId = context.Exercises.First(e => e.Name == "1km Löpning").Id,
                    Toughness = 6
                });
                context.SessionExercises.Add(new SessionExercise()
                {
                    SessionId = context.Sessions.First(s => s.Name == "Cykling").Id,
                    ExerciseId = context.Exercises.First(e => e.Name == "2km Cykling").Id,
                    Toughness = 4
                });
                context.SessionExercises.Add(new SessionExercise()
                {
                    SessionId = context.Sessions.First(s => s.Name == "Simning").Id,
                    ExerciseId = context.Exercises.First(e => e.Name == "200m Simning").Id,
                    Toughness = 4
                });

                context.Equipment.Add(new Equipment() { Name = "Cykel" });
                context.Equipment.Add(new Equipment() { Name = "Barncykel" });
                context.Equipment.Add(new Equipment() { Name = "Skor" });
                context.Equipment.Add(new Equipment() { Name = "Barnskor" });
                context.Equipment.Add(new Equipment() { Name = "Hantlar" });

                context.SaveChanges();

                context.SessionEquipment.Add(new SessionEquipment()
                {
                    SessionId = context.Sessions.First(s => s.Name == "Cykling").Id,
                    EquipmentId = context.Equipment.First(e => e.Name == "Cykel").Id
                });
                context.SessionEquipment.Add(new SessionEquipment()
                {
                    SessionId = context.Sessions.First(s => s.Name == "Löpning").Id,
                    EquipmentId = context.Equipment.First(e => e.Name == "Skor").Id
                });
                context.SessionEquipment.Add(new SessionEquipment()
                {
                    SessionId = context.Sessions.First(s => s.Name == "Lite Blandat").Id,
                    EquipmentId = context.Equipment.First(e => e.Name == "Hantlar").Id
                });
                context.SessionEquipment.Add(new SessionEquipment()
                {
                    SessionId = context.Sessions.First(s => s.Name == "Lite Blandat").Id,
                    EquipmentId = context.Equipment.First(e => e.Name == "Barnskor").Id
                });

                context.SaveChanges();
            }
        }
    }
}
