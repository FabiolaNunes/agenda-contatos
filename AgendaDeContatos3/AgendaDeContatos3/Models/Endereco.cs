using AgendaDeContatos.Models;

public class Endereco : BaseEntity
{
    public string Rua { get; set; }

    public string Numero { get; set; }

    public string Bairro { get; set; }

    public string Cidade { get; set; }

    public string Estado { get; set; }

    public string CEP { get; set; }

    public int ContatoId { get; set; }

    public Contato Contato { get; set; }
}