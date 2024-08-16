
using Newtonsoft.Json.Linq;

public class DataBaseSetter
{
    public static async Task<List<string>> GetDatabaseNames()
    {
        string githubRepoUrl = "https://api.github.com/repos/bryanpimenta/league-of-legends-database/contents/db";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

            var response = await client.GetAsync(githubRepoUrl);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JArray.Parse(jsonResponse);

            return data.Select(item => (string)item["name"])
                    .Where(name => name.StartsWith("patch"))
                    .ToList();
        }
    }
}