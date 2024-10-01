using System.ComponentModel.DataAnnotations;

namespace AgendaDeContatos.Services;

public class ContatoValidator : IContatoValidator
{
    public Task<ValidationResult> ValidarContatoAsync(Contato contato)
    {
        var errors = new List<string>();

        if (contato == null)
        {
            errors.Add("O corpo da solicitação não pode ser nulo.");
        }
        else
        {
            if (string.IsNullOrWhiteSpace(contato.Nome))
            {
                errors.Add("O nome é obrigatório.");
            }

            if (contato.Nome.Length > 100)
            {
                errors.Add("O nome deve ter no máximo 100 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(contato.Email) || !new EmailAddressAttribute().IsValid(contato.Email))
            {
                errors.Add("O e-mail fornecido não é válido.");
            }

            if (string.IsNullOrWhiteSpace(contato.Telefone) || !contato.Telefone.All(char.IsDigit))
            {
                errors.Add("O telefone deve conter apenas números.");
            }
        }

        var isValid = errors.Count == 0;
        return Task.FromResult(new ValidationResult(isValid, errors));
    }
}

public class ValidationResult
{
    public bool IsValid { get; }
    public IEnumerable<string> Errors { get; }

    public ValidationResult(bool isValid, IEnumerable<string> errors)
    {
        IsValid = isValid;
        Errors = errors;
    }
}