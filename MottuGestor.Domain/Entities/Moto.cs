
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MottuGestor.Domain.Enums;

namespace MottuGestor.Domain.Entities
{
    public class Moto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Placa { get; private set; } = string.Empty;
        public StatusMoto Status { get; private set; }
        public string UsuarioId { get; private set; } = string.Empty;

        public List<Patio> Patios { get; private set; } = new();

        private Moto() { }

        public Moto(string placa, StatusMoto status, string usuarioId)
        {
            if (string.IsNullOrWhiteSpace(placa))
                throw new ArgumentException("Placa é obrigatória.", nameof(placa));

            Placa = placa.Trim().ToUpper();
            Status = status;
            UsuarioId = usuarioId;
        }

        public void AtualizarPlaca(string novaPlaca)
        {
            if (string.IsNullOrWhiteSpace(novaPlaca))
                throw new ArgumentException("Placa é obrigatória.", nameof(novaPlaca));
            Placa = novaPlaca.Trim().ToUpper();
        }

        public void AtualizarStatus(StatusMoto novoStatus)
        {
            Status = novoStatus;
        }
    }
}
