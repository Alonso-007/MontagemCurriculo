using System.Collections.Generic;

namespace MontagemCurriculo.Models
{
    public class TipoCurso
    {
        public int TipoCursoId { get; set; }
        public string Tipo { get; set; }
        public ICollection<FormacaoAcademica> FormacaoAcademicas { get; set; }
    }
}