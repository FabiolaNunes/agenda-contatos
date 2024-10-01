namespace AgendaDeContatos.Services;

public class EnderecoValidator : IEnderecoValidator
{
    public Task<ValidationResult> ValidarEnderecoAsync(Endereco endereco)
    {
        var errors = new List<string>();

        if (endereco == null)
        {
            errors.Add("O corpo da solicitação não pode ser nulo.");
        }
        else
        {
            if (string.IsNullOrWhiteSpace(endereco.Rua))
            {
                errors.Add("A rua é obrigatória.");
            }

            if (string.IsNullOrWhiteSpace(endereco.Numero))
            {
                errors.Add("O número é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(endereco.Bairro))
            {
                errors.Add("O bairro é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(endereco.Cidade))
            {
                errors.Add("A cidade é obrigatória.");
            }

            if (string.IsNullOrWhiteSpace(endereco.Estado))
            {
                errors.Add("O estado é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(endereco.CEP) || !endereco.CEP.All(char.IsDigit))
            {
                errors.Add("O CEP deve conter apenas números.");
            }
        }

        var isValid = errors.Count == 0;
        return Task.FromResult(new ValidationResult(isValid, errors));
    }
}