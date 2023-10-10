using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTutorialsConsole
{
    public class Student
    {
       
        public Student ()
        {
            Photo = new byte[] { 0x30, 0x32, 0x32 };
        }


        //Sempre que se usa no nome da propriedade o final Id, o EF entende que deverá
        //usar essa propriedade como chave primária da tabela
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[] Photo { get; set; }

        public decimal Height { get; set; }

        public float Weight { get; set; }


        #region Foreing Key do BD - Apenas para Mapear para o BD
        public Grade Grade { get; set; }

        public int GradeId { get; set; }

        #endregion



    }
}
