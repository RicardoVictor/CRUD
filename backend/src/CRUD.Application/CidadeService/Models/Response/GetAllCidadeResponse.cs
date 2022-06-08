using System.Collections.Generic;
using CRUD.Application.Common;
using CRUD.Application.CidadeService.Models.Dtos;

namespace CRUD.Application.CidadeService.Models.Response
{
    public class GetAllCidadeResponse: BaseResponse
    {
        public List<CidadeDto> Cidades { get; set; }
    }
}
