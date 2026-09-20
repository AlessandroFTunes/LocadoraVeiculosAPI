# Locadora de Veículos — Etapa 1: Modelagem do Banco de Dados

Projeto: sistema de aluguel de veículos (C# / ASP.NET Core / Entity Framework Core / SQL Server Express).

## 1.1. Modelo Conceitual

Entidades e relacionamentos:

- **Fabricante** (1) ──< (N) **Veiculo**
  Todo veículo pertence a um único fabricante.

- **Cliente** (1) ──< (N) **Aluguel**
  Um cliente pode realizar vários aluguéis; todo aluguel pertence a exatamente um cliente.

- **Veiculo** (1) ──< (N) **Aluguel**
  Um veículo pode ser alugado várias vezes (em períodos distintos); todo aluguel se refere a exatamente um veículo.

- **Aluguel** (1) ──── (0..1) **Devolucao**
  Cada aluguel tem no máximo uma devolução registrada (registrada quando o veículo é efetivamente devolvido).

```
Fabricante 1 ---- N Veiculo 1 ---- N Aluguel N ---- 1 Cliente
                                      |
                                      1
                                      |
                                      0..1
                                  Devolucao
```

### Atributos por entidade

**Fabricante**
- Id (PK)
- Nome
- PaisOrigem

**Veiculo**
- Id (PK)
- Modelo
- AnoFabricacao
- Quilometragem
- Placa (único)
- FabricanteId (FK -> Fabricante)

**Cliente**
- Id (PK)
- Nome
- Cpf (único)
- Email
- Telefone

**Aluguel**
- Id (PK)
- ClienteId (FK -> Cliente)
- VeiculoId (FK -> Veiculo)
- DataInicio
- DataFimPrevista
- QuilometragemInicial
- ValorDiaria
- ValorTotal

**Devolucao** (5ª entidade, além das citadas no enunciado)
- Id (PK)
- AluguelId (FK -> Aluguel, único — relação 1:1)
- DataDevolucao
- QuilometragemFinal
- Observacoes

A devolução foi modelada como entidade própria (em vez de colunas soltas em Aluguel) porque representa
um evento distinto, que só existe depois que o aluguel é encerrado, e agrupa naturalmente os dados
coletados nesse momento (data da devolução, quilometragem final, observações sobre o estado do veículo).

## 1.2 – 1.4. Tradução para o banco relacional

O modelo conceitual foi implementado em C# (Code First) na pasta `Models/`, e a `ApplicationContext`
(pasta `Data/`) mapeia essas classes para tabelas relacionais via Entity Framework Core, com Fluent API
definindo explicitamente chaves primárias, chaves estrangeiras e índices únicos (`OnModelCreating`).

Uma migração inicial (`Migrations/InitialCreate`) já foi gerada com `dotnet ef migrations add InitialCreate`,
confirmando que o modelo é traduzido corretamente para o esquema relacional do SQL Server:

- 5 tabelas: `Fabricantes`, `Veiculos`, `Clientes`, `Alugueis`, `Devolucoes`.
- Chaves primárias `Id` (identity) em todas as tabelas.
- Chaves estrangeiras: `Veiculos.FabricanteId`, `Alugueis.ClienteId`, `Alugueis.VeiculoId`, `Devolucoes.AluguelId`.
- Restrições de integridade: `Placa` e `Cpf` únicos; `Devolucoes.AluguelId` único (garante 1:1 com Aluguel);
  `DeleteBehavior.Restrict` nas FKs de `Aluguel` (evita múltiplos caminhos de cascade delete no SQL Server).

## Como aplicar o banco de dados (SQL Server Express)

1. Ajuste a connection string em `appsettings.json` se necessário (por padrão aponta para `.\SQLEXPRESS`).
2. Rode:
   ```
   dotnet ef database update
   ```
   Isso cria o banco `LocadoraVeiculosDb` com as 5 tabelas modeladas.
