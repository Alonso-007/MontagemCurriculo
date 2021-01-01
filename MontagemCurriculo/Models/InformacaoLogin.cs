namespace MontagemCurriculo.Models
{
    public class InformacaoLogin
    {
        public int InformacaoLoginId { get; set; }
        public string EnderecoIp { get; set; }
        public string Data { get; set; }
        public string Horario { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}