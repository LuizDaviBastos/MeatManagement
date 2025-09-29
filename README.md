# MeatManager - Documentação de Inicialização

Este projeto é composto por um **Backend ASP.NET** e um **Frontend React**.

## Estrutura do Repositório

```
/API
  └─ MeatManagement.sln       # Backend ASP.NET
/App
  └─ meat-management-app      # Frontend React
```

## Pré-requisitos

Antes de rodar o projeto, instale:

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) ou versão compatível.
* [Node.js](https://nodejs.org/) (>= 18).
* [npm](https://www.npmjs.com/) ou [yarn](https://yarnpkg.com/).
* SQL Server

## Backend (ASP.NET)

1. Abra o terminal no diretório `/API`:

```bash
cd API
```

2. Restaure os pacotes NuGet:

```bash
dotnet restore MeatManagement.sln
```

3. Configure a string de conexão em `appsettings.json` ou variáveis de ambiente:

```json
"ConnectionStrings": {
  "MeatManagerDatabase": "Server=localhost;Database=MeatManager;Trusted_Connection=True;"
}
```

5. Rode a API:

```bash
dotnet run --project MeatManagement/MeatManager.API.csproj
```

O backend estará disponível em `http://localhost:5049`.

## Frontend (React)

1. Abra o terminal no diretório `/App/meat-management-app`:

```bash
cd App/meat-management-app
```

2. Instale as dependências:

```bash
npm install
# ou
yarn install
```

3. Configure a URL da API em `.env` (crie se não existir):

```env
REACT_APP_API_URL=http://localhost:5049
```

4. Rode a aplicação React:

```bash
npm start
# ou
yarn start
```

O frontend estará disponível em `http://localhost:3000`.

## Observações

* Scripts necessários para a criação do banco de dados [MeatManager_InitDB.sql](./API/MeatManagement.Data/Init/MeatManager_InitDB.sql).
