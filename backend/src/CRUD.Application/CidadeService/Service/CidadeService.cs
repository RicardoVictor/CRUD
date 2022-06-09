using CRUD.Application.Common;
using CRUD.Application.CidadeService.Models.Dtos;
using CRUD.Application.CidadeService.Models.Request;
using CRUD.Application.CidadeService.Models.Response;
using CRUD.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CRUD.Application.CRUDAggregate;

namespace CRUD.Application.CidadeService.Service
{
    public class CidadeService : BaseService<CidadeService>, ICidadeService
    {
        private readonly CRUDContext _db;

        public CidadeService(ILogger<CidadeService> logger, Infra.Data.CRUDContext db) : base(logger)
        {
            _db = db;
        }

        public async Task<GetAllCidadeResponse> GetAllAsync()
        {
            var entities = await _db.Cidade.ToListAsync();
            return new GetAllCidadeResponse()
            {
                Cidades = entities != null ? entities.Select(a => (CidadeDto)a).ToList() : new List<CidadeDto>()
            };
        }

        public async Task<GetByIdCidadeResponse> GetByIdAsync(int id)
        {
            var response = new GetByIdCidadeResponse();

            var entity = await _db.Cidade.FirstOrDefaultAsync(item => item.Id == id);

            if (entity == null)
                throw new NotFoundException("Cidade não encontrada para o id: " + id);

            response.Cidade = (CidadeDto)entity;

            return response;
        }

        public async Task<CreateCidadeResponse> CreateAsync(CreateCidadeRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request empty!");

            var newCidade = Domain.CRUDAggregate.Cidade.Create(request.Nome, request.UF);

            if (_db.Cidade.Any())
                newCidade.Id = _db.Cidade.Max(c => c.Id) + 1;
            else
                newCidade.Id = 1;

            _db.Cidade.Add(newCidade);

            await _db.SaveChangesAsync();

            return new CreateCidadeResponse() { Id = newCidade.Id };
        }

        public async Task<UpdateCidadeResponse> UpdateAsync(int id, UpdateCidadeRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request empty!");

            var entity = await _db.Cidade.FirstOrDefaultAsync(item => item.Id == id);

            if (entity != null)
            {
                entity.Update(request.Nome, request.UF);
                await _db.SaveChangesAsync();
            }

            return new UpdateCidadeResponse();
        }

        public async Task<DeleteCidadeResponse> DeleteAsync(int id)
        {
            var entity = await _db.Cidade.FirstOrDefaultAsync(item => item.Id == id);

            if (entity == null)
                throw new NotFoundException("Cidade não encontrada para o id: " + id);
            
            if(await _db.Pessoa.AnyAsync(p => p.Id_Cidade == id))
                throw new ArgumentException("Cidade não pode ser deletada pois esta referenciada em Pessoa.");

            _db.Remove(entity);
            await _db.SaveChangesAsync();

            return new DeleteCidadeResponse();
        }
    }
}
