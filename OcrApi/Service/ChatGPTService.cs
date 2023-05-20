using OcrApi.Model;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace OcrApi.Service
{
    public class ChatGPTService
    {
        public Pessoa ExtrairDados(string texto)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string scriptPath = Path.Combine(currentDirectory, "chatgpt_script.py");
            Pessoa pessoa = new Pessoa();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"{scriptPath} \"{texto}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                using (var reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(result))
                    {
                        var resultSplit = result.Split(',');
                        pessoa.Nome = resultSplit[0];
                        pessoa.Cpf = resultSplit[1];
                        pessoa.NomeMae = resultSplit[2];

                        var dataSplit = resultSplit[3].Split('/');
                        string ano = Regex.Match(dataSplit[2], @"\b\d+\b").Value;
                        pessoa.DataNascimento = new DateTime(
                            Convert.ToInt32(ano),
                            Convert.ToInt32(dataSplit[1]),
                            Convert.ToInt32(dataSplit[0]));
                    }
                }
            }
            return pessoa;
        }
    }
}
