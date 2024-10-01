using Microsoft.AspNetCore.Mvc;
using AgendaDeContatos.Repositories;
using AgendaDeContatos.Services;

namespace AgendaDeContatos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContatosController : ControllerBase
{
    private readonly IContatoRepository _repository;
    private readonly IContatoValidator _validator;
    private readonly IEnderecoValidator _enderecoValidator;
    private readonly IEnderecoRepository _enderecoRepository;

    public ContatosController(IContatoRepository repository, IContatoValidator validator, IEnderecoValidator enderecoValidator, IEnderecoRepository enderecoRepository)
    {
        _repository = repository;
        _validator = validator;
        _enderecoValidator = enderecoValidator;
        _enderecoRepository = enderecoRepository;
       
    }

    [HttpGet]
    public async Task<IActionResult> ListarContatos()
    {
        var contatos = await _repository.ListarTodosAsync();
        return Ok(contatos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var contato = await _repository.BuscarPorIdAsync(id);
        if (contato == null)
        {
            return NotFound("Contato não encontrado.");
        }
        return Ok(contato);
    }

    [HttpGet("Buscar-endereco/{id}")]
    public async Task<IActionResult> BuscarEnderecoPorId(int id)
    {
        var endereco = await _enderecoRepository.BuscarPorIdAsync(id);
        if (endereco == null)
            return NotFound("Endereço não encontrado.");

        return Ok(endereco);
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarContato([FromBody] ContatoDto contato)
    {
        if (contato == null)
        {
            return BadRequest("O corpo da solicitação não pode ser nulo.");
        }

        var novoContato = new Contato()
        {
            Nome = contato.Nome,
            Email = contato.Email,
            Telefone = contato.Telefone,
        };

        // Validar o contato
        var validationResult = await _validator.ValidarContatoAsync(novoContato);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _repository.AdicionarAsync(novoContato);
        return CreatedAtAction(nameof(BuscarPorId), new { id = novoContato.Id }, new { mensagem = "Contato criado com sucesso." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarContato(int id, [FromBody] ContatoDto contatoAtualizado)
    {
        if (contatoAtualizado == null)
        {
            return BadRequest("O corpo da solicitação não pode ser nulo.");
        }

        var contatoExistente = await _repository.BuscarPorIdAsync(id);
        if (contatoExistente == null)
        {
            return NotFound("Contato não encontrado.");
        }

        contatoExistente.Nome = contatoAtualizado.Nome;
        contatoExistente.Email = contatoAtualizado.Email;
        contatoExistente.Telefone = contatoAtualizado.Telefone;

        var validationResult = await _validator.ValidarContatoAsync(contatoExistente);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _repository.SaveAsync();
        return Ok(new { mensagem = "Contato editado com sucesso." });
    }

    [HttpPut("enderecos-editar/{id}")]
    public async Task<IActionResult> EditarEndereco(int id, [FromBody] EnderecoDto enderecoAtualizado)
    {
        if (enderecoAtualizado == null)
        {
            return BadRequest("O corpo da solicitação não pode ser nulo.");
        }

        var enderecoExistente = await _enderecoRepository.BuscarPorIdAsync(id);
        if (enderecoExistente == null)
        {
            return NotFound("Endereço não encontrado.");
        }

        enderecoExistente.Cidade = enderecoAtualizado.Cidade;
        enderecoExistente.Rua = enderecoAtualizado.Rua;
        enderecoExistente.CEP = enderecoAtualizado.CEP;
        enderecoExistente.Numero = enderecoAtualizado.Numero;
        enderecoExistente.Estado = enderecoAtualizado.Estado;
        enderecoExistente.Bairro = enderecoAtualizado.Bairro;

        var validationResult = await _enderecoValidator.ValidarEnderecoAsync(enderecoExistente);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _enderecoRepository.AtualizarAsync(enderecoExistente);
        return Ok(new { mensagem = "Endereço editado com sucesso." });
    }

    [HttpPut("adicionar-endereco/{id}")]
    public async Task<IActionResult> AdicionarEndereco(int id, [FromBody] EnderecoDto endereco)
    {
        if (endereco == null)
        {
            return BadRequest("O corpo da solicitação não pode ser nulo.");
        }

        var contatoExistente = await _repository.BuscarPorIdAsync(id);
        if (contatoExistente == null)
        {
            return NotFound("Contato não encontrado.");
        }

        var novoEndereco = new Endereco()
        {
            Cidade = endereco.Cidade,
            Rua = endereco.Rua,
            CEP = endereco.CEP,
            Numero = endereco.Numero,
            Estado = endereco.Estado,
            Bairro = endereco.Bairro,
            ContatoId = contatoExistente.Id,
            Contato = contatoExistente,
        };

        contatoExistente.Enderecos.Add(novoEndereco);

        await _repository.SaveAsync();
        return Ok(new { mensagem = "Endereco adicionado com sucesso." });
    }

    [HttpDelete("enderecos/{id}")]
    public async Task<IActionResult> ExcluirEndereco(int id)
    {
        var enderecoExistente = await _enderecoRepository.BuscarPorIdAsync(id);
        if (enderecoExistente == null)
        {
            return NotFound("Endereço não encontrado.");
        }

        await _enderecoRepository.ExcluirAsync(id);
        return Ok(new { mensagem = "Endereço excluído com sucesso." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirContato(int id)
    {
        var contatoExistente = await _repository.BuscarPorIdAsync(id);
        if (contatoExistente == null)
        {
            return NotFound("Contato não encontrado.");
        }

        await _repository.ExcluirAsync(id);
        return Ok(new { mensagem = "Contato apagado com sucesso." });
    }
}