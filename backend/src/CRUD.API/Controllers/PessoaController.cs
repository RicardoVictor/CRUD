using CRUD.Application.CidadeService.Service;
using CRUD.Application.CRUDAggregate;
using CRUD.Application.PessoaService.Models.Request;
using CRUD.Application.PessoaService.Models.Response;
using CRUD.Application.PessoaService.Service;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : BaseController
    {
        private readonly IPessoaService _service;

        public PessoaController(ILogger<PessoaController> logger, IPessoaService service) : base()
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var action = await _service.GetAllAsync();
                return Ok(action);
            }
            catch (Exception ex)
            {
                return ExceptionHandling(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var action = await _service.GetByIdAsync(id);
                return Ok(action);
            }
            catch (ArgumentException ex)
            {
                return ArgumentExceptionHandling(ex);
            }
            catch (Exception ex)
            {
                return ExceptionHandling(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePessoaRequest request)
        {
            try
            {
                var action = await _service.CreateAsync(request);
                return Ok(action);
            }
            catch (NotFoundException ex)
            {
                return NotFoundExceptionHandling(ex);
            }
            catch (ArgumentException ex)
            {
                return ArgumentExceptionHandling(ex);
            }
            catch (Exception ex)
            {
                return ExceptionHandling(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePessoaRequest request)
        {
            try
            {
                var action = await _service.UpdateAsync(id, request);
                return Ok(action);
            }
            catch (ArgumentException ex)
            {
                return ArgumentExceptionHandling(ex);
            }
            catch (Exception ex)
            {
                return ExceptionHandling(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var action = await _service.DeleteAsync(id);
                return Ok(action);
            }
            catch (ArgumentException ex)
            {
                return ArgumentExceptionHandling(ex);
            }
            catch (Exception ex)
            {
                return ExceptionHandling(ex);
            }
        }
    }
}
