using CRUD.Domain.CRUDAggregate;

namespace CRUD.Application.CidadeService.Models.Dtos
{
    public class CidadeDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }

        public static explicit operator CidadeDto(Domain.CRUDAggregate.Cidade v)
        {
            return new CidadeDto()
            {
                Id = v.Id,
                Nome = v.Nome,
                UF = v.UF
            };
        }
    }
}
