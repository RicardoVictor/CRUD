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
        // public virtual ICollection<Pessoa> Pessoas { get; set; }

        public static Cidade Create(string nome, string uf)
        {
            if (nome == null)
                throw new ArgumentException("Invalid " + nameof(nome));

            if (uf == null)
                throw new ArgumentException("Invalid " + nameof(uf));

            if (uf.Length > 2)
                throw new InvalidUFExceptions();

            return new Cidade(nome, uf);
        }

        public void Update(string nome, string uf)
        {
            if (nome != null)
                Nome = nome;

            if (uf.Length > 2)
                throw new InvalidUFExceptions();

            if (uf != null)
                UF = uf;
        }
    }
}
