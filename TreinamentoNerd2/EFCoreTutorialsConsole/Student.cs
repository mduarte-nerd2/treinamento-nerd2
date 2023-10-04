using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTutorialsConsole
{
    public class Student
    {
        //Sempre que se usa no nome da propriedade o final Id, o EF entende que deverá
        //usar essa propriedade como chave primária da tabela
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        #region Foreing Key do BD - Apenas para Mapear para o BD
        public Grade Grade { get; set; }

        public int GradeId { get; set; }

        #endregion



    }
}
