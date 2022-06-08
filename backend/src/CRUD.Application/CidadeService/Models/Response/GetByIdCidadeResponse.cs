using CRUD.Application.Common;
using CRUD.Application.CidadeService.Models.Dtos;

namespace CRUD.Application.CidadeService.Models.Response
{
    public class GetByIdCidadeResponse : BaseResponse
    {
        public CidadeDto Cidade { get; set; }
    }
}
