using System.ComponentModel.DataAnnotations;

namespace MontagemCurriculo.Models
{
    public class FormacaoAcademica
    {
        public int FormacaoAcademicaId { get; set; }
        public int TipoCursoId { get; set; }
        public TipoCurso TipoCurso { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        public string Instituicao { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1920, 2018, ErrorMessage = "Ano inválido")]
        public int AnoInicio { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1920, 2018, ErrorMessage = "Ano inválido")]
        public int AnoFim { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        public string NomeCurso { get; set; }
        public int CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }
    }
}