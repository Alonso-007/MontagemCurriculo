using System.ComponentModel.DataAnnotations;

namespace MontagemCurriculo.Models
{
    public class Objetivo
    {
        public int ObjetivoId { get; set; }
        [Required(ErrorMessage = "Descrição obrigatória")]
        [StringLength(1000, ErrorMessage = "Descrição muito longa")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }
        public int CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }
    }
}