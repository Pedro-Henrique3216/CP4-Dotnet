# Integrantes

- Pedro Henrique dos Santos RM: 559064
- Thiago Thomaz RM: 557992

# 🏍️ Mottu Challenge - Gestão de Pátio e Setores

Este projeto implementa um sistema de **gestão de pátio (Yard)**, **setores (Sector)** e **vagas (Spots)** para organização e alocação de motos.  
O objetivo é permitir que filiais da Mottu consigam estruturar seus pátios em setores e, automaticamente, gerar as vagas disponíveis para as motos.

---

## 📌 Domínio

- **Yard (Pátio)**  
  Representa um espaço físico de uma filial, que pode conter múltiplos setores.  
  Cada pátio possui dimensões e restrições de coordenadas.

- **Sector (Setor)**  
  Representa uma área dentro de um pátio.  
  É definido por pontos (polígono), e a partir dele são geradas vagas (spots).  
  O sistema valida se o setor:
  - Está contido dentro do pátio.  
  - Não se sobrepõe a outros setores do mesmo pátio.  

- **Spot (Vaga)**  
  Representa uma vaga de moto dentro de um setor.  
  Por padrão, cada vaga ocupa um espaço de **2m x 2m**.  
  Exemplo: um setor de 10m x 10m comporta 25 vagas.
  
  - **Employee (Funcionario)**  
    Representa o funcionario 

---

## ⚙️ Instruções de Execução

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Banco de dados MySQL
- [EF Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- - MongoDB (usado por partes do projeto e pelo health-check)

### Passos
1. Clone o repositório:
   ```bash
   git clone https://github.com/Pedro-Henrique3216/CP4-Dotnet.git
   cd mottu-challenge
   ```
   Abra a solução
2. Configure a connection string no appsettings.json:
  ```bash
  "ConnectionStrings": {
  "MySqlConnection": "server=localhost;uid={seu usuario};pwd={sua senha};database={nome do database}"
  }
  ```
3. Abra o Package menager console que fica no tools NuGet Package Menage e rode esse comando
   ```bash
   Update-Database -Project MottuChallenge.Infrastructure -StartupProject MottuChallenge.Api
   ```
4. Start o Programa

---

### Como rodar o MongoDB

Opção 1 — MongoDB instalado localmente

Instale o MongoDB na sua máquina (via instalador oficial ou pacote). Garanta que o serviço esteja rodando e que a connection string em `appsettings.json` (Settings.MongoDb.ConnectionString) aponte para ele.

Opção 2 — MongoDB via Docker (recomendado para desenvolvimento)

No PowerShell:

```powershell
docker run -d --name mongodb -p 27017:27017 -v mongodbdata:/data/db mongo:6.0
```

Isso expõe o MongoDB em `localhost:27017`. Configure em `appsettings.json` uma connection string como `mongodb://localhost:27017` e defina `Settings.MongoDb.DatabaseName`.

---

## Health-check

A API expõe um endpoint de health-check configurado em:

- GET /api/health-check

O health-check realiza checagens incluindo:

- Conexão com MySQL (nome: "mysql connection")
- Conexão com MongoDB (nome: "mongo connection")
- Disponibilidade da ViaCep (nome: "Via Cep API")

Exemplos (substitua a base URL se necessário):

curl:

```bash
curl -k https://localhost:5001/api/health-check
```
----------------------------

## Testes

POST /api/yards
Content-Type: application/json

```json{
  "name": "Pátio Central",
  "cep": "01311300",
  "number": "100",
  "points": [
    { "pointOrder": 1, "x": 0, "y": 0 },
    { "pointOrder": 2, "x": 0, "y": 50 },
    { "pointOrder": 3, "x": 50, "y": 50 },
    { "pointOrder": 4, "x": 50, "y": 0 }
  ]
}
```

Aqui ele usa a api do via cep para buscar o endereço da pessoa

POST /api/sectors_type
Content-Type: application/json

```json
{
  "name": "Estacionamento"
}
```

Tem validação se ja existe sector_type com esse nome

POST /api/sectors
Content-Type: application/json

```json
{
  "yardId": "id gerado quando cria o yard",
  "sectorTypeId": "id gerado quando cria sectorType",
  "points": [
    {
      "pointOrder": 1,
      "x": 0,
      "y": 0
    },
    {
      "pointOrder": 2,
      "x": 0,
      "y": 10
    },
    {
      "pointOrder": 3,
      "x": 10,
      "y": 10
    },
    {
      "pointOrder": 4,
      "x": 10,
      "y": 0
    }
  ]
}
```

com isso sera gerado o maximo de vagas disponiveis para a dimensão do setor, aqui tem validação se o setor cabe dentro do patio ou se ja tem um setor cadastrado nesse lugar.

## Endpoints de Employee (versão 2)

A API de funcionários está disponível na versão 2:

- Base: `/api/v2/employees`

Endpoints principais:

- GET `/api/v2/employees` — listar todos os funcionários
- GET `/api/v2/employees/{email}` — buscar funcionário por email
- POST `/api/v2/employees` — criar funcionário
- PUT `/api/v2/employees` — atualizar funcionário
- DELETE `/api/v2/employees/{email}` — remover funcionário por email

Exemplo de payload (POST/PUT):

```json
{
  "name": "João Silva",
  "email": "joao@example.com",
  "yardId": "<id-do-yard>",
  "password": "senha123"
}
```

Exemplos rápidos (substitua a base URL conforme seu ambiente):




  
