# 🛒 Desafio Ambev - Sistema de Registro de Vendas

Este projeto é uma API .NET 8 que gerencia registros de vendas com aplicação de regras de desconto por quantidade, seguindo princípios de DDD, eventos simulados e testes automatizados.

---

## ✅ Funcionalidades

- Registro de vendas com múltiplos itens
- Regras de desconto automáticas:
  - 4 a 9 unidades → 10%
  - 10 a 20 unidades → 20%
  - Máximo de 20 unidades por item
- Cancelamento de venda
- Simulação de eventos:
  - `SaleCreated`
  - `SaleCancelled`
  - `ItemCancelled` (estrutura preparada)
- Testes automatizados com xUnit

---

## 🧱 Tecnologias

- [.NET 8](https://dotnet.microsoft.com)
- [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://docs.fluentvalidation.net)
- [xUnit](https://xunit.net)
- [AutoMapper](https://automapper.org)
- [NSubstitute](https://nsubstitute.github.io)
- Docker + Docker Compose

---

## 🚀 Como Executar

### 🔧 Clonar o Repositório

```bash
git clone https://github.com/LucasMagaldi/Desafio-ambev.git
cd Desafio-ambev
```

## Rodar com Docker (Recomendado)

```bash
docker-compose  up -d
```

Acessar o container

```bash
docker exec -it ambev_developer_evaluation_sdk bash
```

Instalar CLI do Entity Framework no container

```bash
dotnet tool install --global dotnet-ef
```

```bash
export PATH="$PATH:/root/.dotnet/tools"
```

Aplicar as migrations

```bash
  cd /src/src/Ambev.DeveloperEvaluation.WebApi
  dotnet ef database update --project ../Ambev.DeveloperEvaluation. --startup-project . --context DefaultContext
```

Acesse o Swagger para testar a API:

```bash
https://localhost:8081/swagger/index.html
```

## 🖥️ Rodar Localmente (Sem Docker)

Instale o .NET 8 SDK:
👉 Download .NET 8

Navegue até o projeto WebApi e execute:

```bash
cd src/Ambev.DeveloperEvaluation.WebApi
dotnet run
```

A API estará disponível em:

```bash
https://localhost:8081/swagger
```

🧪 Rodar os Testes Unitários

```bash
dotnet test tests/Ambev.DeveloperEvaluation.Unit/Ambev.DeveloperEvaluation.Unit.csproj
```
