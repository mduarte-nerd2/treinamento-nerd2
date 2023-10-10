using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net.Http.Headers;

namespace EFCoreTutorialsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entre com o primeiro nome do estudante:");
            var nomeEstudante = Console.ReadLine();

            Console.WriteLine("Entre com o sobrenome do estudante:");
            var sobrenomeEstudante = Console.ReadLine();

            CriarEntidade(nomeEstudante, sobrenomeEstudante);

            var std = new Student { FirstName = nomeEstudante, LastName = sobrenomeEstudante + "-Detached" };

            CriarEntidadeDesatachadaDoContexto(std);


            ModificarEntidade(nomeEstudante, sobrenomeEstudante);

            ExcluirEntidade();

            GetPeloNome(nomeEstudante);

            GetPeloNomeComInclude(nomeEstudante);



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


        private static void CriarEntidade(string nome, string sobrenome)
        {
            using (SchoolContext context = new SchoolContext())
            {
                //Criar BD se não existir
                //context.Database.EnsureCreated();

                //Criar uma entidade de objeto
                Grade grade01 = new Grade() { GradeName = "Curso C# .Net" };
                Student student01 = new Student() { FirstName = nome, LastName = sobrenome, Grade = grade01 };

                //Adicionar a entidade ao contexto
                context.Students.Add(student01);

                //Salvar a entidade estudante
                context.SaveChanges();

                foreach (Student std in context.Students)
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


        private static void ModificarEntidade(string novoNome, string novoSobrenome)
        {
            try
            {

                using (SchoolContext ctx = new SchoolContext())
                {

                    var std = ctx.Students.First<Student>();
                    std.FirstName = novoNome;
                    std.LastName = novoSobrenome;

                    ctx.SaveChanges();
                }

            }
            catch (DbUpdateConcurrencyException ex01)
            {
                //throw new Exception("Record does not exist in the database");
                Console.WriteLine($"Registro nao existe no banco de dados {ex01.Message}");

            }
            catch (Exception ex02)
            {
                Console.WriteLine($"Ocorreu a seguinte exceção: {ex02.Message}");
            }


        }

        private static void ExcluirEntidade()
        {
            using (SchoolContext schoolContext = new SchoolContext())
            {
                var std = schoolContext.Students.First<Student>();
                schoolContext.Students.Remove(std);
                // ou
                // schoolContext.Remove<Student>(std);

                schoolContext.SaveChanges();

            }
        }

        private static void GetPeloNome(string nome)
        {
            var context = new SchoolContext();
            List<Student> studentsWithSameName = context.Students.Where(std => std.FirstName == nome).ToList();

            Console.WriteLine("Listando Estudante...");
            foreach (var item in studentsWithSameName)
            {
                Console.WriteLine(item.FirstName);
            }
        }


        private static void GetPeloNomeComInclude(string nome)
        {
            var context = new SchoolContext();

            var studentWithGrade = context.Students
                                       .Where(s => s.FirstName == nome)
                                       .Include(s => s.Grade)
                                       .FirstOrDefault();

            Console.WriteLine("...Listando Estudante com Grade usando Include... " +
               studentWithGrade.FirstName + " - " + studentWithGrade.Grade.GradeName);
           
        }

        private static void CriarEntidadeDesatachadaDoContexto(Student std)
        {
            std.Grade = new Grade { GradeName = "Outra Materia" };
            
            using (var ctx = new SchoolContext())
            {
                ctx.Add<Student>(std);

                ctx.SaveChanges();

            }

        }

    }
}