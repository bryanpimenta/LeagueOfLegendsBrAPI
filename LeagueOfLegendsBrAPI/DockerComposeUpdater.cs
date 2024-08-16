using System;
using System.IO;
using System.Linq;

namespace LeagueOfLegendsBrAPI
{
    public static class DockerComposeUpdater
    {
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
