using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Domain.CRUDAggregate
{
    public class Pessoa
    {
        private Pessoa(
            string nome,
            string cpf,
            int idade,
            int idCidade)
        {
            this.Nome = nome;
            this.CPF = cpf;
            this.Idade = idade;
            this.Id_Cidade = idCidade;
        }

        private Pessoa()
        {
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int Idade { get; set; }
        public int Id_Cidade { get; set; }

        public virtual Cidade Cidade { get; set; }

        public static Pessoa Create(
            string nome,
            string cpf,
            int idade,
            int idCidade)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Invalid " + nameof(nome));

            if (nome.Length > 300)
                throw new ArgumentException(nameof(nome) + " deve ser menor ou igual a 300 caracteres.");

            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("Invalid " + nameof(cpf));
                
            if (cpf.Length > 11)
                throw new ArgumentException(nameof(cpf) + " deve ser menor ou igual a 11 caracteres.");

            if (idade <= 0)
                throw new ArgumentException(nameof(idade) + " deve ser maior que zero.");

            return new Pessoa(nome, cpf, idade, idCidade);
        }

        public void Update(
            string nome,
            string cpf,
            int idade,
            int idCidade)
        {            
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(nameof(nome) + "não pode ser null ou vazio.");

            if (nome.Length > 300)
                throw new ArgumentException(nameof(nome) + " deve ser menor ou igual a 300 caracteres.");

            if (nome != null)
                Nome = nome;

            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException(nameof(cpf) + "não pode ser null ou vazio.");

            if (cpf.Length > 11)
                throw new ArgumentException(nameof(cpf) + " deve ser menor ou igual a 11 caracteres.");

            if (cpf != null)
                CPF = cpf;

            if (idade <= 0)
                throw new ArgumentException(nameof(idade) + " deve ser maior que zero.");

            Idade = idade;
            Id_Cidade = idCidade;
        }
    }
}
