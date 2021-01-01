using MontagemCurriculo.Models;
using System.Collections.Generic;

namespace MontagemCurriculo.ViewModels
{
    public class CurriculoViewModel
    {
        public IEnumerable<Objetivo> Objetivos { get; set; }
        public IEnumerable<FormacaoAcademica> FormacoesAcademicas { get; set; }
        public IEnumerable<ExperienciaProfissional> ExperienciasProfissionais { get; set; }
        public IEnumerable<Idioma> Idiomas { get; set; }
    }
}