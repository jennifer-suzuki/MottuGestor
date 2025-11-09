
# Challenge: MottuGestor

## Projeto: API REST para Gestão de Motos e Pátios - Mottu

Este projeto da disciplina Advanced Business Development with .NET tem como objetivo desenvolver uma API RESTful utilizando .NET 8 e MongoDB.

Uma API que permite a gestão completa das motos, pátios e usuários, com funcionalidades para cadastrar, consultar, atualizar e deletar registros.

## Integrantes

- Felipe Levy Stephens Fidelix - RM556426
- Jennifer Kaori Suzuki  - RM554661
- Pedro Henrique Jorge de Paula - RM558833

---

## Estrutura

- **API**: Controllers, Validações de entrada e Configurações do Swagger.
- **Application**: DTOs e handlers.
- **Domain**: Entidades, Enums, Value objects e Interfaces.
- **Infrastructure**: Acesso a dados (MongoDB) e Serviços externos.  

---

## Tecnologias Utilizadas

- .NET 8  
- C#  
- Entity Framework Core (EF Core)  
- Docker
- MongoDB
- Swagger / OpenAPI  
- Rider (JetBrains)

---

## Como rodar o projeto

```bash
# 1. Clonar o repositório
git clone https://github.com/jennifer-suzuki/MottuGestor.git
cd MottuGestor

# 2. Restaurar e dar build no projeto
dotnet restore
dotnet build

# 3. Rodar a API
dotnet run --project MottuGestor.API
```

## Autenticação

Faça uma requisição Post no Auth
```json
{
  "email": "email@gmail.com",
  "senha": "senha123"
}
```

Copie o token retornado e cole no botão "Authorize" no formato:
```
Bearer {token}
```


## Endpoints e Testes de exemplo:

### Moto
| Método | Endpoint           | Descrição                       |
|--------|--------------------|--------------------------------|
| GET    | /api/Moto         | Lista todas as motos                        |
| GET    | /api/Moto/{id}    | Consulta moto por ID                        |
| POST   | /api/Moto         | Cadastra nova moto                          |
| PUT    | /api/Moto/{id}    | Atualiza dados de uma moto                  |
| DELETE | /api/Moto/{id}    | Remove uma moto pelo ID                     |


**Exemplo POST**
```json
{
  "rfid": "RFID1294",
  "placa": "DKS9256",
  "status": 2,
  "usuarioId": "0"
}
```

**Exemplo PUT**
```json
{
  "rfid": "RFID8214",
  "placa": "PDH6520",
  "status": 1,
  "usuarioId": "0"
}
```

### Patio
| Método | Endpoint           | Descrição                       |
|--------|--------------------|--------------------------------|
| GET    | /api/Patio         | Lista todos os pátios cadastrados          |
| GET    | /api/Patio/{id}    | Retorna os dados de um pátio pelo ID       |
| POST   | /api/Patio         | Cadastra um novo pátio                     |
| PUT    | /api/Patio/{id}    | Atualiza os dados de um pátio existente    |
| DELETE | /api/Patio/{id}    | Remove um pátio do sistema pelo ID         |


**Exemplo POST**
```json

{
  "endereco": "Rua das Araucárias, 990",
  "usuarioId": "0",
  "motoId": "0"
}
```
### Usuario
| Método | Endpoint           | Descrição                       |
|--------|--------------------|--------------------------------|
| GET    | /api/Usuario         | Lista todos os usuários cadastrados      |
| GET    | /api/Usuario/{id}    | Consulta usuário por ID                  |
| POST   | /api/Usuario         | Cadastra novo usuário                    |
| PUT    | /api/Usuario/{id}    | Atualiza dados de um usuário             |
| DELETE | /api/Usuario/{id}    | Remove uma usuário pelo ID               |


**Exemplo POST**
```json
{
  "nome": "Márcio da Silva",
  "email": "marcio@gmail.com"
}
```

## Execução dos testes:

Para executar os testes, use o seguinte comando:

```bash
dotnet test
```
