using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCoreTutorialsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CriarEntidade();
            ListarEstadoUnchangedEntidades();
            ListarEstadoAddedEntidades();

            ListarEstadoModifiedEntidades();

            ListarEstadoDeletedEntidades();

            ListarEstadoDetachedEntidades();

        }

        private static void ListarEstadoDetachedEntidades()
        {
            var disconnectedEntity = new Student() { StudentId = 1, FirstName = "Bill" };

            using (var context = new SchoolContext())
            {
                Console.Write(context.Entry(disconnectedEntity).State);
            }
        }

        private static void ListarEstadoDeletedEntidades()
        {
            using (var context = new SchoolContext())
            {
                var student = context.Students.FirstOrDefault();
                context.Students.Remove(student);

                DisplayStates(context.ChangeTracker.Entries());
            }
        }

        private static void ListarEstadoModifiedEntidades()
        {
            using (var context = new SchoolContext())
            {
                var student = context.Students.FirstOrDefault();
                student.LastName = "Friss";

                DisplayStates(context.ChangeTracker.Entries());
            }
        }

        private static void ListarEstadoAddedEntidades()
        {
            using (var context = new SchoolContext())
            {
                context.Students.Add(new Student() { FirstName = "Bill", LastName = "Gates" });

                DisplayStates(context.ChangeTracker.Entries());
            }
        }


        private static void CriarEntidade()
        {
            using (SchoolContext context = new SchoolContext())
            {
                //Criar BD se não existir
                context.Database.EnsureCreated();

                //Criar uma entidade de objeto
                var grade01 = new Grade() { GradeName = "Curso C# .Net" };
                var student01 = new Student() { FirstName = "Joaninha", LastName = "Silveirinha", Grade = grade01 };

                //Adicionar a entidade ao contexto
                context.Students.Add(student01);

                //Salvar a entidade estudante
                context.SaveChanges();

                foreach (var std in context.Students)
                {
                    Console.WriteLine(std.FirstName + " " + std.LastName);

                }

            }
        }

        private static void ListarEstadoUnchangedEntidades()
        {
            using (var context = new SchoolContext())
            {
                // retrieve entity 
                var student = context.Students.FirstOrDefault();
                DisplayStates(context.ChangeTracker.Entries());
            }
        }

        static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Entidade: {entry.Entity.GetType().Name}, " +
                    $"State: {entry.State.ToString()}");
            }
        }
    }
}