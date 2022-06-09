using CRUD.Application.Common;
using CRUD.Application.CRUDAggregate;
using CRUD.Application.PessoaService.Models.Dtos;
using CRUD.Application.PessoaService.Models.Request;
using CRUD.Application.PessoaService.Models.Response;
using CRUD.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRUD.Application.PessoaService.Service
{
    public class PessoaService : BaseService<PessoaService>, IPessoaService
    {
        private readonly CRUDContext _db;

        public PessoaService(ILogger<PessoaService> logger, Infra.Data.CRUDContext db) : base(logger)
        {
            _db = db;
        }

        public async Task<GetAllPessoaResponse> GetAllAsync()
        {
            var entities = await _db.Pessoa.Include(p => p.Cidade).ToListAsync();

            return new GetAllPessoaResponse()
            {
                Pessoas = entities != null ? entities.Select(a => (PessoaDto)a).ToList() : new List<PessoaDto>()
            };
        }

        public async Task<GetByIdPessoaResponse> GetByIdAsync(int id)
        {
            var response = new GetByIdPessoaResponse();

            var entity = await _db.Pessoa.FirstOrDefaultAsync(item => item.Id == id);

            if (entity == null)
                throw new NotFoundException("Pessoa não encontrada para o id: " + id);

            response.Pessoa = (PessoaDto)entity;

            return response;
        }

        public async Task<CreatePessoaResponse> CreateAsync(CreatePessoaRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request empty!");

            var cidade = await _db.Cidade.FirstOrDefaultAsync(c => c.Id == request.IdCidade);

            if (cidade == null)
                throw new NotFoundException("Cidade não encontrada para o id: " + request.IdCidade);

            var newPessoa = Domain.CRUDAggregate.Pessoa.Create(request.Nome, request.CPF, request.Idade, request.IdCidade);

            if (_db.Pessoa.Any())
                newPessoa.Id = _db.Pessoa.Max(c => c.Id) + 1;
            else
                newPessoa.Id = 1;

            _db.Pessoa.Add(newPessoa);

            await _db.SaveChangesAsync();

            return new CreatePessoaResponse() { Id = newPessoa.Id };
        }

        public async Task<UpdatePessoaResponse> UpdateAsync(int id, UpdatePessoaRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request empty!");

            var entity = await _db.Pessoa.FirstOrDefaultAsync(item => item.Id == id);

            if (entity == null)
                throw new NotFoundException("Pessoa não encontrada para o id: " + id);

            var cidade = await _db.Cidade.FirstOrDefaultAsync(c => c.Id == request.IdCidade);

            if (cidade == null)
                throw new NotFoundException("Cidade não encontrada para o id: " + request.IdCidade);

            entity.Update(request.Nome, request.CPF, request.Idade, request.IdCidade);
            await _db.SaveChangesAsync();

            return new UpdatePessoaResponse()
            {
                Pessoa = new PessoaDto()
                {
                    Id = entity.Id,
                    Nome = entity.Nome,
                    CPF = entity.CPF,
                    Idade = entity.Idade,
                    Cidade = cidade
                }
            };
        }

        public async Task<DeletePessoaResponse> DeleteAsync(int id)
        {
            var entity = await _db.Pessoa.FirstOrDefaultAsync(item => item.Id == id);

            if (entity == null)
                throw new NotFoundException("Pessoa não encontrada para o id: " + id);

            _db.Remove(entity);
            await _db.SaveChangesAsync();

            return new DeletePessoaResponse();
        }
    }
}
