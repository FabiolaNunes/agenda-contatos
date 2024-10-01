using AgendaDeContatos.Models;

public class Contato : BaseEntity
{
    public string Nome { get; set; }

    public string Email { get; set; }

    public string Telefone { get; set; }

    public ICollection<Endereco> Enderecos { get; set; } = 
        new List<Endereco>();

}