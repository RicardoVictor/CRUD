using CRUD.Application.PessoaService.Models.Request;
using CRUD.Application.PessoaService.Models.Response;
using System.Threading.Tasks;

namespace CRUD.Application.PessoaService.Service
{
    public interface IPessoaService
    {
        Task<GetAllPessoaResponse> GetAllAsync();
        Task<GetByIdPessoaResponse> GetByIdAsync(int id);
        Task<CreatePessoaResponse> CreateAsync(CreatePessoaRequest request);
        Task<UpdatePessoaResponse> UpdateAsync(int id, UpdatePessoaRequest request);
        Task<DeletePessoaResponse> DeleteAsync(int id);
    }
}
