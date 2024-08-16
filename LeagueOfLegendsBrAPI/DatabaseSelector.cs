using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LeagueOfLegendsBrAPI
{
    public static class DatabaseSelector
    {
        public static async Task<string> SelectDatabaseAsync()
        {
            string[] possiblePaths = {
                Path.Combine(Directory.GetCurrentDirectory(), "../database"),
                "/app/database"
            };
            
            string selectedDatabase = null;
            string databaseFolderPath = null;
            string[] existingDatabases = null;

            foreach (var path in possiblePaths)
            {
                if (Directory.Exists(path))
                {
                    databaseFolderPath = path;
                    existingDatabases = Directory.GetFiles(databaseFolderPath, "*.sql");
                    break;
                }
            }

            if (existingDatabases == null)
            {
                Console.WriteLine("Nenhuma pasta de banco de dados encontrada.");
                return null;
            }

            var databaseNames = await DataBaseSetter.GetDatabaseNames();

            if (databaseNames.Count > existingDatabases.Length)
            {
                Console.WriteLine("Baixando todos os bancos de dados disponíveis...");
                foreach (var databaseName in databaseNames)
                {
                    if (!existingDatabases.Any(db => Path.GetFileName(db) == databaseName))
                    {
                        await DownloadDatabaseAsync(databaseName, databaseFolderPath);
                    }
                }

                existingDatabases = Directory.GetFiles(databaseFolderPath, "*.sql");
            }

            Array.Sort(existingDatabases, (a, b) => File.GetLastWriteTime(b).CompareTo(File.GetLastWriteTime(a)));

            Console.WriteLine("Escolha um dos seguintes bancos de dados:");
            for (int i = 0; i < existingDatabases.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(existingDatabases[i])}");
            }

            int selectedIndex = await GetUserInputAsync(existingDatabases.Length);
            selectedDatabase = existingDatabases[selectedIndex];

            return selectedDatabase;
        }

        private static async Task<int> GetUserInputAsync(int maxIndex)
        {
            int selectedIndex = -1;
            var timeoutTask = Task.Delay(10000);
            var inputTask = Task.Run(() =>
            {
                while (selectedIndex == -1)
                {
                    Console.Write("\nDigite o número correspondente ao banco de dados desejado: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out selectedIndex) && selectedIndex > 0 && selectedIndex <= maxIndex)
                    {
                        selectedIndex--;
                        break;
                    }
                    Console.WriteLine("Entrada inválida. Tente novamente.");
                }
            });

            await Task.WhenAny(inputTask, timeoutTask);

            if (!inputTask.IsCompleted)
            {
                selectedIndex = 0;
                Console.WriteLine($"\nTempo esgotado! Selecionando o mais atual automaticamente: {selectedIndex + 1}");
            }

            return selectedIndex;
        }

        private static async Task DownloadDatabaseAsync(string databaseName, string databaseFolderPath)
        {
            string githubRawUrl = $"https://raw.githubusercontent.com/bryanpimenta/league-of-legends-database/main/db/{databaseName}";
            Directory.CreateDirectory(databaseFolderPath);
            string filePath = Path.Combine(databaseFolderPath, $"{Path.GetFileNameWithoutExtension(databaseName)}.sql");

            using (var client = new HttpClient())
            {
                Console.WriteLine($"\nBaixando o arquivo {databaseName}...");
                var response = await client.GetAsync(githubRawUrl);
                response.EnsureSuccessStatusCode();

                var fileContent = await response.Content.ReadAsStringAsync();
                await File.WriteAllTextAsync(filePath, fileContent);

                Console.WriteLine($"Arquivo salvo com sucesso em: {filePath}");
            }
        }
    }
}
