using System.Collections.Generic;
using CRUD.Application.Common;
using CRUD.Application.PessoaService.Models.Dtos;

namespace CRUD.Application.PessoaService.Models.Response
{
    public class GetAllPessoaResponse: BaseResponse
    {
        public List<PessoaDto> Pessoas { get; set; }
    }
}
