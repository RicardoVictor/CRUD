using CRUD.Application.Common;
using CRUD.Application.PessoaService.Models.Dtos;

namespace CRUD.Application.PessoaService.Models.Response
{
    public class GetByIdPessoaResponse : BaseResponse
    {
        public PessoaDto Pessoa { get; set; }
    }
}
