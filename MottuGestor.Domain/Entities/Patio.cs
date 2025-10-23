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

        public string Endereco { get; private set; } = string.Empty;
        public string UsuarioId { get; private set; } = string.Empty;
        public string MotoId { get; private set; } = string.Empty;

        private Patio()
        {
        }

        public Patio(string endereco, string usuarioId, string motoId)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("Endereço é obrigatório.", nameof(endereco));

            Endereco = endereco.Trim();
            UsuarioId = usuarioId;
            MotoId = motoId;
        }
    }
}