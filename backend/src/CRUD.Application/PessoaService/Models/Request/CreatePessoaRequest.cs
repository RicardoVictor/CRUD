namespace CRUD.Application.PessoaService.Models.Request
{
    public class CreatePessoaRequest
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int Idade { get; set; }
        public int IdCidade { get; set; }
    }
}
