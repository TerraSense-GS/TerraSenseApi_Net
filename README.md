# TerraSense API

## Integrantes

**Nome:** Agatha Yie Won Yun  
**RM:** 561507  
**Turma:** 2TDSA

**Nome:** Ana Claudia Fernandes Martins  
**RM:** 561190  
**Turma:** 2TDSR

**Nome:** André Rosa Colombo  
**RM:**  563112  
**Turma:** 2TDSA

**Nome:** Samantha Faruolo Galdi  
**RM:** 554794  
**Turma:** 2TDSA

**Nome:** Vitor Fria Dalmagro  
**RM:** 566052  
**Turma:** 2TDSA

# Sobre

API REST desenvolvida em ASP.NET Core para gerenciamento de relatórios ambientais do sistema TerraSense, utilizando Oracle Database com Entity Framework Core.

O sistema permite armazenar relatórios ambientais das plantações, registrando informações como NDVI, status geral, temperatura, umidade, chuva, radiação solar, propriedade, plantação e cidade. Também permite adicionar observações vinculadas a cada relatório.

O objetivo da API é manter um histórico dessas informações para consultas futuras e acompanhamento das condições ambientais das plantações.

---

# Tecnologias Utilizadas

- ASP.NET Core (.NET 10)
- Entity Framework Core
- Oracle Database
- Swagger / OpenAPI
- C#
- Rider
- GitHub

---

# Funcionalidades

## Relatórios

- Cadastrar relatório
- Listar relatórios
- Buscar relatório por ID
- Buscar relatórios por plantação
- Atualizar relatório
- Remover relatório

## Observações

- Cadastrar observação para um relatório
- Listar observações
- Buscar observação por ID
- Remover observação

---

# Regras de Negócio

- Um relatório representa um registro histórico das informações ambientais de uma plantação.
- Um relatório pode possuir várias observações.
- Uma observação deve estar vinculada a um relatório existente.
- Ao excluir um relatório, suas observações também são removidas automaticamente.
- Ao excluir uma observação, apenas ela é removida, mantendo o relatório existente.
- A data de criação da observação é registrada automaticamente pela API.

---

# Banco de Dados

O projeto utiliza Oracle Database integrado com Entity Framework Core.

A persistência dos dados é feita em banco relacional por meio das tabelas:

- `TB_RELATORIO_PLANTACAO`
- `TB_OBSERVACAO_RELATORIO`

## Relacionamento do Banco de Dados

O projeto possui um relacionamento **1:N** entre relatórios e observações.

- Um relatório pode possuir várias observações.
- Uma observação pertence a apenas um relatório.

### Diagrama Entidade-Relacionamento 

<img width="1230" height="1279" alt="image" src="https://github.com/user-attachments/assets/b7a95867-d89f-400c-adfc-17ae5bf89a29" />

---

# Migrations

O projeto utiliza migrations do Entity Framework Core para criação e atualização das tabelas no Oracle.

Comandos utilizados:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

As migrations foram utilizadas para criar automaticamente as tabelas, chaves primárias, chave estrangeira e o relacionamento 1:N entre relatórios e observações no banco Oracle.

---

# OpenAPI / Swagger

A documentação da API está disponível via Swagger.

Exemplo:

```bash
http://localhost:5056/swagger
```

A porta pode variar conforme a execução do projeto.

---

# Documentação das Rotas

## Relatórios

### Listar todos os relatórios

```http
GET /api/relatorios
```

Retorna todos os relatórios cadastrados, incluindo suas observações.

---

### Buscar relatório por ID

```http
GET /api/relatorios/{id}
```

Retorna o relatório correspondente ao ID informado.

---

### Buscar relatórios por plantação

```http
GET /api/relatorios/plantacao/{idPlantacao}
```

Retorna todos os relatórios vinculados a uma plantação específica.

---

### Criar relatório

```http
POST /api/relatorios
```

Exemplo:

```json
{
  "idPlantacao": 1,
  "nomePlantacao": "Plantação de Soja",
  "nomePropriedade": "Fazenda Terra Verde",
  "cidade": "Goiânia",
  "ndvi": 0.72,
  "statusGeral": "NORMAL",
  "temperatura": 28.5,
  "umidade": 55.0,
  "chuva": 1.2,
  "radiacaoSolar": 780.5,
  "observacoes": []
}
```

Cria um novo relatório ambiental.

---

### Atualizar relatório

```http
PUT /api/relatorios/{id}
```

Exemplo:

```json
{
  "idRelatorio": 1,
  "idPlantacao": 1,
  "nomePlantacao": "Plantação de Milho",
  "nomePropriedade": "Fazenda TerraSense",
  "cidade": "Goiânia",
  "ndvi": 0.85,
  "statusGeral": "ATENCAO",
  "temperatura": 33.5,
  "umidade": 38.0,
  "chuva": 0.0,
  "radiacaoSolar": 920.0,
  "dataRelatorio": "2026-06-07T20:00:00",
  "observacoes": []
}
```

Atualiza os dados de um relatório existente.

---

### Remover relatório

```http
DELETE /api/relatorios/{id}
```

Remove o relatório informado.  
As observações vinculadas a ele também são removidas automaticamente.

---

## Observações

### Listar todas as observações

```http
GET /api/observacoes
```

Retorna todas as observações cadastradas.

---

### Buscar observação por ID

```http
GET /api/observacoes/{id}
```

Retorna a observação correspondente ao ID informado.

---

### Criar observação

```http
POST /api/observacoes
```

Exemplo:

```json
{
  "descricao": "Monitorar irrigação nos próximos dias.",
  "relatorioPlantacaoId": 1
}
```

Cria uma observação vinculada a um relatório existente.

---

### Remover observação

```http
DELETE /api/observacoes/{id}
```

Remove apenas a observação informada.  
O relatório relacionado continua existindo.

---

# Tratamento de Erros

A API possui validações básicas para evitar operações inválidas.

Exemplo:

- Caso seja informada uma observação com `relatorioPlantacaoId` inexistente, a API retorna erro `400 Bad Request`.
- Caso seja buscado um relatório ou observação inexistente, a API retorna `404 Not Found`.
- Caso o ID informado no PUT seja diferente do ID do objeto enviado, a API retorna `400 Bad Request`.

---

# Como Instalar e Executar

## 1. Clonar o repositório - COLOCAR URL

```bash
git clone URL_DO_REPOSITORIO
```

## 2. Entrar na pasta do projeto

```bash
cd TerraSenseApi
```

## 3. Restaurar dependências

```bash
dotnet restore
```

## 4. Configurar o banco Oracle

No arquivo `appsettings.json`, configurar a string de conexão:

```json
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL;"
  }
}
```

## 5. Executar migrations

```bash
dotnet ef database update
```

## 6. Executar o projeto

Pelo terminal:

```bash
dotnet run
```

Ou pelo Rider, utilizando o botão **Run**.

## 7. Acessar o Swagger

Após iniciar a API, acessar:

```bash
http://localhost:5056/swagger
```

---

# Testes Realizados

Foram realizados testes utilizando o Swagger para validar o funcionamento da API.

## Relatórios

- Criação de relatório com `POST /api/relatorios`
- Listagem de relatórios com `GET /api/relatorios`
- Busca por ID com `GET /api/relatorios/{id}`
- Busca por plantação com `GET /api/relatorios/plantacao/{idPlantacao}`
- Atualização de relatório com `PUT /api/relatorios/{id}`
- Remoção de relatório com `DELETE /api/relatorios/{id}`

## Observações

- Criação de observação com `POST /api/observacoes`
- Listagem de observações com `GET /api/observacoes`
- Busca por ID com `GET /api/observacoes/{id}`
- Remoção de observação com `DELETE /api/observacoes/{id}`

## Relacionamento

Também foi testado o relacionamento entre relatório e observações.

Ao buscar um relatório por ID, suas observações vinculadas são exibidas junto ao relatório.

Ao excluir um relatório, as observações relacionadas são removidas automaticamente.

---

# Prints dos Testes - FAZER AS PRINTS

Adicionar prints de:

- Swagger aberto com os endpoints
- POST de relatório
- GET de relatório
- PUT de relatório
- DELETE de relatório
- POST de observação
- GET de observação
- DELETE de observação
- Consulta das tabelas no Oracle
- Migration criada no projeto

---

# Estrutura do Projeto

```text
TerraSenseApi
│
├── TerraSenseApi
│   ├── Dependencies
│   │   ├── Imports
│   │   └── .NET 10.0
│   ├── Properties
│   │   └── launchSettings.json
│   ├── Controllers
│   │   ├── ObservacoesController.cs
│   │   └── RelatoriosController.cs
│   │
│   ├── Data
│   │   └── AppDbContext.cs
│   │
│   ├── Migrations
│   │   ├── 20260607230635_InitialCreate.cs
│   │   │   └── 20260607230635_InitialCreate.Designer.cs
│   │   └── AppDbContextModelSnapshot.cs
│   │
│   ├── Models
│   │   ├── ObservacaoRelatorio.cs
│   │   └── RelatorioPlantacao.cs
│   │
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Program.cs
│   └── TerraSenseApi.http
│
└── README.md
```

---

# Melhorias Futuras

- Integração com a API Java do TerraSense
- Geração automática de relatórios a partir dos dados do sistema principal
- Integração com dados IoT e satélite em tempo real
- Dashboard para visualização histórica dos relatórios
- Filtros por data, plantação, cidade e status
- Exportação de relatórios em PDF
- Notificações automáticas para relatórios com status crítico

---

# Observações

A API foi desenvolvida como um módulo complementar ao sistema TerraSense.

Enquanto a API Java é responsável pelo gerenciamento principal das entidades do sistema, esta API em .NET é responsável pelo armazenamento histórico dos relatórios ambientais das plantações, permitindo consultas futuras e registro de observações relacionadas.
