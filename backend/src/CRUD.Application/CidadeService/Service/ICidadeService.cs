using CRUD.Application.CidadeService.Models.Request;
using CRUD.Application.CidadeService.Models.Response;
using System.Threading.Tasks;

namespace CRUD.Application.CidadeService.Service
{
    public interface ICidadeService
    {
        Task<GetAllCidadeResponse> GetAllAsync();
        Task<GetByIdCidadeResponse> GetByIdAsync(int id);
        Task<CreateCidadeResponse> CreateAsync(CreateCidadeRequest request);
        Task<UpdateCidadeResponse> UpdateAsync(int id, UpdateCidadeRequest request);
        Task<DeleteCidadeResponse> DeleteAsync(int id);
    }
}
