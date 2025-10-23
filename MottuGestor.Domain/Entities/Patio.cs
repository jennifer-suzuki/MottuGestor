using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MottuGestor.Domain.ValueObjects;

namespace MottuGestor.Domain.Entities
{
    public class Patio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Descricao { get; private set; } = string.Empty;
        public DateTime Data { get; private set; } = DateTime.UtcNow;
        public string UsuarioId { get; private set; } = string.Empty;
        public string MotoId { get; private set; } = string.Empty;

        private Patio()
        {
        }

        public Patio(string descricao, DateTime data, string usuarioId, string motoId)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));
            if (data == default) data = DateTime.UtcNow;

            Descricao = descricao.Trim();
            Data = data;
            UsuarioId = usuarioId;
            MotoId = motoId;
        }

        public void Reagendar(DateTime novaData)
        {
            if (novaData < DateTime.UtcNow.AddDays(-1))
                throw new ArgumentException("Não é permitido agendar no passado distante.");
            Data = novaData;
        }
    }
}