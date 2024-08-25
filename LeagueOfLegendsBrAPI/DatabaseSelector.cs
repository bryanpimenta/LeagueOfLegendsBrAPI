using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LeagueOfLegendsBrAPI
{
    /// <summary>
    /// Classe que permite selecionar um banco de dados disponível localmente ou baixá-lo automaticamente do meu outro repositório GitHub.
    /// </summary>
    public static class DatabaseSelector
    {
        /// <summary>
        /// Seleciona um banco de dados SQL a partir de uma pasta local ou baixa novos bancos de dados, caso não estejam presentes.
        /// </summary>
        /// <returns>
        /// O caminho completo do banco de dados selecionado, ou <c>null</c> se nenhum banco de dados foi encontrado.
        /// </returns>
        /// <remarks>
        /// Este método verifica se existem arquivos SQL nas pastas. Se não houver arquivos locais, ele faz o download de bancos de dados adicionais 
        /// do meu outro repositório GitHub. O método permite ao usuário selecionar um banco de dados da lista de arquivos disponíveis, com um tempo limite de 10 segundos para escolha automática.
        /// </remarks>
        public static async Task<string?> SelectDatabaseAsync()
        {
            string[] possiblePaths = {
                Path.Combine(Directory.GetCurrentDirectory(), "../database"),
                "/app/database"
            };

            string? selectedDatabase = null;
            string databaseFolderPath = string.Empty;
            string[] existingDatabases = Array.Empty<string>();

            foreach (var path in possiblePaths)
            {
                if (Directory.Exists(path))
                {
                    databaseFolderPath = path;
                    existingDatabases = Directory.GetFiles(databaseFolderPath, "*.sql");
                    break;
                }
            }

            if (existingDatabases.Length == 0)
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

        /// <summary>
        /// Obtém a escolha do usuário para selecionar um banco de dados ou seleciona automaticamente após um tempo limite.
        /// </summary>
        /// <param name="maxIndex">O número máximo de opções disponíveis para seleção.</param>
        /// <returns>O índice do banco de dados selecionado pelo usuário ou automaticamente após o tempo limite.</returns>
        /// <remarks>
        /// Este método aguarda a entrada do usuário para selecionar o banco de dados desejado. Se o usuário não fizer uma escolha em 10 segundos, 
        /// o banco de dados mais recente é selecionado automaticamente.
        /// </remarks>
        private static async Task<int> GetUserInputAsync(int maxIndex)
        {
            int selectedIndex = -1;
            var timeoutTask = Task.Delay(10000);
            var inputTask = Task.Run(() =>
            {
                while (selectedIndex == -1)
                {
                    Console.Write("\nDigite o número correspondente ao banco de dados desejado: ");
                    string? input = Console.ReadLine();

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

        /// <summary>
        /// Faz o download de um arquivo de banco de dados SQL do meu outro repositório GitHub e o salva em uma pasta local.
        /// </summary>
        /// <param name="databaseName">O nome do arquivo de banco de dados a ser baixado.</param>
        /// <param name="databaseFolderPath">O caminho da pasta onde o arquivo será salvo.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona de download.</returns>
        /// <remarks>
        /// Este método constrói a URL de um arquivo de banco de dados no repositório GitHub, baixa o arquivo e o salva na pasta local especificada.
        /// </remarks>
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
