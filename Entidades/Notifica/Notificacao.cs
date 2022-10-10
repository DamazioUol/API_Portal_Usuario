using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades.Notifica
{
    public class Notificacao
    {
        public Notificacao()
        {
            Notificacoes = new List<Notificacao>();
        }

        [NotMapped]
        [Ignore]
        [JsonIgnore]
        public string? NomePropriedade { get; set; }

        [NotMapped]
        [Ignore]
        [JsonIgnore]
        public string? Mensagem { get; set; }

        [NotMapped]
        [Ignore]
        [JsonIgnore]
        public List<Notificacao>? Notificacoes { get; set; }


        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {

            if (string.IsNullOrEmpty(valor) || string.IsNullOrEmpty(nomePropriedade))
            {
                Notificacoes.Add(new Notificacao
                {
                    Mensagem = $"{nomePropriedade} - Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });
            }

            return true;
        }

        public bool ValidarPropriedadeDataNascimento(DateTime data, string nomePropriedade)
        {
            if (data > DateTime.UtcNow || string.IsNullOrEmpty(nomePropriedade))
            {
                Notificacoes.Add(new Notificacao
                {
                    Mensagem = "Data de nascimento não pode ser maior que a data atual.",
                    NomePropriedade = nomePropriedade
                });

                return false;
            };

            return true;
        }

        public bool ValidarPropriedadeEmail(string valor, string nomePropriedade)
        {
            if (
                string.IsNullOrEmpty(valor) ||
                !valor.Contains("@") ||
                !valor.Contains(".com") ||
                string.IsNullOrEmpty(nomePropriedade)
                )
            {
                Notificacoes.Add(new Notificacao
                {
                    Mensagem = "Email inválido.",
                    NomePropriedade = nomePropriedade
                });

                return false;
            };


            return true;
        }
    }
}
