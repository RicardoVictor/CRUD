using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Domain.CRUDAggregate
{
    public class Cidade
    {
        private Cidade(string nome, string uf)
        {
            this.Nome = nome;
            this.UF = uf;
        }

        private Cidade()
        {
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }

        public static Cidade Create(string nome, string uf)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(nameof(nome) + " não pode ser null ou vazio.");

            if (nome.Length > 200)
                throw new ArgumentException(nameof(nome) + " deve ser menor ou igual a 200 caracteres.");

            if (string.IsNullOrWhiteSpace(uf))
                throw new ArgumentException(nameof(uf) + " não pode ser null ou vazio.");

            if (uf.Length > 2)
                throw new InvalidUFExceptions();

            return new Cidade(nome, uf);
        }

        public void Update(string nome, string uf)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(nameof(nome) + " não pode ser null ou vazio.");

            if (nome.Length > 200)
                throw new ArgumentException(nameof(nome) + " deve ser menor ou igual a 200 caracteres.");

            if (string.IsNullOrWhiteSpace(uf))
                throw new ArgumentException(nameof(uf) + " não pode ser null ou vazio.");

            if (uf.Length > 2)
                throw new InvalidUFExceptions();

            Nome = nome;
            UF = uf;
        }
    }
}
