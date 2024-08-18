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
- Requisitos
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
```dotnet restore```

Rodando a Aplicação
```dotnet run```

A API estará disponível em http://localhost:5000.

Configurando o Banco de Dados
Verifique se o MySQL está rodando e configure a string de conexão no arquivo appsettings.json.

Opcionalmente, use o Docker para rodar o banco de dados. Certifique-se de configurar o docker-compose.yml adequadamente:
```docker-compose up -d```


Uso
Documentação da API
Após rodar a aplicação, você pode acessar a documentação interativa da API gerada pelo Swagger:
```http://localhost:5000/swagger```

Ele trás todas rotas da API.
