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

---

## ⚙️ Instruções de Execução

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Banco de dados MySQL
- [EF Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Passos
1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/mottu-challenge.git](https://github.com/Pedro-Henrique3216/CP4-Dotnet/
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




  
