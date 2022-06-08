using CRUD.Domain.CRUDAggregate;

namespace CRUD.Application.PessoaService.Models.Dtos
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int Idade { get; set; }
        public Cidade Cidade { get; set; }

        public static explicit operator PessoaDto(Domain.CRUDAggregate.Pessoa v)
        {
            return new PessoaDto()
            {
                Id = v.Id,
                Nome = v.Nome,
                CPF = v.CPF,
                Idade = v.Idade,
                Cidade = v.Cidade
            };
        }
    }
}
