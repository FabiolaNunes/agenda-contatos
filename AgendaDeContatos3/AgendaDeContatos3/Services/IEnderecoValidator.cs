namespace AgendaDeContatos.Services;

public interface IEnderecoValidator
{
    Task<ValidationResult> ValidarEnderecoAsync(Endereco endereco);

}