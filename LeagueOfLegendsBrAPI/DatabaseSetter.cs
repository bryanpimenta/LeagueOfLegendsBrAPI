using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DataBaseSetter
{
    /// <summary>
    /// Está classe obtém uma lista de nomes de arquivos de banco de dados do meu outro repositório GitHub, 
    /// filtrando apenas os que começam com "patch" pois são os que possui campos atualizados como o de links para 3D dos personagens.
    /// </summary>
    /// <returns>
    /// Uma lista de strings contendo os nomes dos arquivos de banco de dados que começam com "patch".
    /// </returns>
    /// <remarks>
    /// Este método faz uma solicitação para a API do GitHub para recuperar o conteúdo do diretório 
    /// "db" no repositório. Ele filtra os arquivos que começam com "patch" e retorna seus 
    /// nomes em uma lista. O método utiliza `HttpClient` para a solicitação e analisa a resposta JSON 
    /// utilizando `JArray` do pacote Newtonsoft.Json.
    /// </remarks>
    public static async Task<List<string>> GetDatabaseNames()
    {
        string githubRepoUrl = "https://api.github.com/repos/bryanpimenta/league-of-legends-database/contents/db";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

            var response = await client.GetAsync(githubRepoUrl);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JArray.Parse(jsonResponse) as JArray;

            return data?.Select(item => (string?)item["name"])
                    .Where(name => name?.StartsWith("patch") == true)
                    .Where(name => name != null)
                    .Cast<string>()
                    .ToList() ?? new List<string>();
        }
    }
}
