namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario
{
    public class UsuarioFormulario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string TipoPessoa { get; set; }
        public string TipoUsuario { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Documento { get; set; }
        public bool Status { get; set; }
    }

    public class UsuarioStatusFormulario
    {
        public string Documento { get; set; }
        public bool Status { get; set; }
    }

    public class CadastrarUsuarioFormulario
    {
        public string documento { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public string telefone { get; set; }
        public string tipo_pessoa { get; set; }
        public string termos { get; set; }
    }
}
