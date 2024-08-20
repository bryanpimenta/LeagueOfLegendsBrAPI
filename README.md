<img src="./img/API.png"><img>
# `LeagueOfLegendsBrAPI`
Está é uma API em C# que fornece informações sobre campeões do jogo League of Legends.

Este projeto tem como objetivo criar um `GET` de informações para fornecer dados detalhados sobre os campeões do League of Legends, como atributos, habilidades e skins. A API permite consultas por nome do campeão, além de fornecer os dados em uma estrutura organizada e de fácil utilização.

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

# Deploy

Utilizei o serviço da <a href="https://cloud.google.com/products/compute?hl=pt-BR" target="_blank">Google Cloud Compute Engine<a>.
</br></br>
Aqui você encontra todas rotas documentadas: <a href="https://app.leagueoflegendsbr.site/swagger/index.html" target="_blank">app.leagueoflegendsbr.site/swagger<a> </br>
</br>
<i>Não abuse de nossa querida API, ela é pequena e tem limite de 25 req por minuto, agradeço a compreensão :)</i>
</br>

# Instalação
Clonando o Repositório
```
git clone https://github.com/bryanpimenta/LeagueOfLegendsBrAPI.git
cd LeagueOfLegendsBrAPI
```

Instalando Dependências
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
</br></br>

#### Detalhe para rodar apenas a aplicação: 
Para subir o banco de dados e rodar `LeagueOfLegendsBrAPI` separado, abra o `appsettings.json` e coloque `server=localhost` ao invez de `db` e siga a orientação mais abaixo de como subir apenas o container do banco de dados.
</br></br>
# Docker
Certifique-se de ter instalado o Docker e o Docker-Compose.
</br></br>
Subindo os Containers:
```
docker-compose up --build
```

Tanto a aplicação quanto o banco de dados serão orquestrados, sem preocupações com outras configurações. 
</br></br>
## Subindo apenas o SQL com Docker
Ótimo para testar querys e views do banco:

```
docker-compose up db --build
```

Configurando o Banco de Dados
Verifique se o MySQL está rodando e configure a string de conexão no arquivo `appsettings.json`.
</br></br>
## Do Banco de dados SQL
O banco de dados é montado apartir do meu outro repo: <a href="https://github.com/bryanpimenta/league-of-legends-database" target="_blank">league-of-legends-database<a>
</br></br>
## Uso
Documentação da API
Após rodar a aplicação, você pode acessar a documentação interativa da API gerada pelo Swagger:
```http://localhost:5050/swagger```
