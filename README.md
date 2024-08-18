# LeagueOfLegendsBrAPI
`LeagueOfLegendsBrAPI` é uma API em C# que fornece informações sobre campeões do jogo League of Legends.

Este projeto tem como objetivo criar um Get de informações para fornecer dados detalhados sobre os campeões do League of Legends, como atributos, habilidades e skins. A API permite consultas por nome do campeão, além de fornecer os dados em uma estrutura organizada e de fácil utilização.

## Tecnologias Utilizadas
- C#
- ASP.NET Core
- Swashbuckle.AspNetCore (para documentação do Swagger)
- Docker
- MySQL
- Entity Framework Core

## Requisitos
- .NET SDK 8.0 ou superior
- Docker (opcional, para execução em container)
- MySQL

# Instalação
Clonando o Repositório
```
git clone https://github.com/bryanpimenta/LeagueOfLegendsBrAPI.git
cd LeagueOfLegendsBrAPI
```

Restaurando Dependências
```
cd LeagueOfLegendsBrAPI
dotnet restore
```

Rodando a Aplicação
```
cd LeagueOfLegendsBrAPI
dotnet run
```

A API estará disponível em:
`http://localhost:5050`

Configurando o Banco de Dados
Verifique se o MySQL está rodando e configure a string de conexão no arquivo `appsettings.json`.

# Docker
Rode o comando:

```docker-compose up --build```

Tanto a aplicação quanto o banco de dados serão orquestrados, sem preocupações com outras configurações. 

## Subindo apenas o SQL com Docker
Ótimo para testar querys e views do banco:

```docker-compose up db --build```

### Detalhe: 
Para subir o banco de dados apenas e rodar LeagueOfLegendsBrAPI separado, abra o `appsettings.json` e coloque `server=localhost` ao invez de db.

## Uso
Documentação da API
Após rodar a aplicação, você pode acessar a documentação interativa da API gerada pelo Swagger:
```http://localhost:5050/swagger```
