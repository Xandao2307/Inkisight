# Sistema de Avaliação e Resenha de Livros

Este é um sistema de avaliação e resenha de livros desenvolvido em C# .NET. O sistema permite que os usuários cadastrem livros, escrevam resenhas e avaliem os livros cadastrados.

## Funcionalidades

- Cadastro e autenticação de usuários.
- Gerenciamento do catálogo de livros.
- Postagem e gerenciamento de resenhas.
- Sistema de avaliação dos livros.
- Pesquisa e filtragem de livros.

## Tecnologias Utilizadas

- .NET Core 7
- Entity Framework Core
- AutoMapper
- Swagger/OpenAPI
- MySQL

## Como Executar o Projeto

### Pré-requisitos

- Visual Studio 2019 ou superior
- .NET Core SDK
- SQL Server (ou utilize Azure SQL Database)
- Conta de serviço para APIs externas (se necessário)

### Passos para Execução

1. Clone este repositório para o seu ambiente local.
2. Abra o projeto utilizando o Visual Studio.
3. Configure a conexão com o banco de dados no arquivo `appsettings.json`.
4. Execute o comando `dotnet restore` para restaurar as dependências do projeto.
5. Execute o comando `dotnet ef database update` para aplicar as migrações e criar o banco de dados.
6. Execute o projeto pressionando `F5` no Visual Studio.
7. Acesse a aplicação em `https://localhost:7135/` no seu navegador web.

## Documentação da API

A documentação da API está disponível utilizando o Swagger/OpenAPI. Após iniciar o projeto, acesse `http://localhost:7135/swagger` para explorar os endpoints disponíveis.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues para reportar bugs, sugerir melhorias ou contribuir com novas funcionalidades. Por favor, siga as diretrizes de contribuição do projeto.

## Licença

Este projeto está licenciado sob a [Licença MIT](LICENSE).
