using System.Collections.Generic;

namespace MontagemCurriculo.Models
{
    public class Curriculo
    {
        public int CurriculoId { get; set; }
        public string Nome { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<Objetivo> Objetivos { get; set; }
        public ICollection<FormacaoAcademica> FormacaoAcademicas { get; set; }
        public ICollection<ExperienciaProfissional> ExperienciaProfissionais { get; set; }
        public ICollection<Idioma> Idiomas { get; set; }

    }
}