using System;
using System.IO;
using System.Linq;

namespace LeagueOfLegendsBrAPI
{
    /// <summary>
    /// Classe que atualiza o arquivo Docker Compose com um novo arquivo de banco de dados puxado do repo de databases.
    /// </summary>
    public static class DockerComposeUpdater
    {
        /// <summary>
        /// Atualiza o arquivo Docker Compose, substituindo o banco de dados 
        /// com base nos nomes fornecidos.
        /// </summary>
        /// <param name="databaseFileName">
        /// O caminho completo ou o nome do arquivo de banco de dados a ser incluído no arquivo Docker Compose.
        /// </param>
        /// <remarks>
        /// Este método busca o arquivo docker-compose.yml em caminhos predefinidos, atualiza o mapeamento do volume 
        /// do banco de dados com o novo nome de arquivo e salva as alterações.
        /// </remarks>
        public static void UpdateDockerCompose(string databaseFileName)
        {
            string[] possiblePaths = {
                Path.Combine(Directory.GetCurrentDirectory(), "../docker-compose.yml"),
                "/app/docker-compose.yml"
            };

            string dockerComposePath = null;

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    dockerComposePath = path;
                    break;
                }
            }

            if (dockerComposePath == null)
            {
                Console.WriteLine("Arquivo docker-compose.yml não encontrado.");
                return;
            }

            string dockerComposeContent = File.ReadAllText(dockerComposePath);

            string formattedFileName = Path.GetFileName(databaseFileName);
            string newLine = $"      - ./database/{formattedFileName}:/docker-entrypoint-initdb.d/{formattedFileName}";

            var lines = dockerComposeContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            bool lineUpdated = false;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("/docker-entrypoint-initdb.d/"))
                {
                    lines[i] = newLine;
                    lineUpdated = true;
                    break;
                }
            }

            if (!lineUpdated)
            {
                lines.Add(newLine);
            }

            dockerComposeContent = string.Join('\n', lines);
            File.WriteAllText(dockerComposePath, dockerComposeContent);
            Console.WriteLine($"\nArquivo docker-compose.yml atualizado com o banco de dados: {formattedFileName}");
        }
    }
}

