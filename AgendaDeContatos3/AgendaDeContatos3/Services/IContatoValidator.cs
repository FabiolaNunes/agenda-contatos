namespace AgendaDeContatos.Services;

public interface IContatoValidator
{
    Task<ValidationResult> ValidarContatoAsync(Contato contato);

}