using Google.Cloud.Vision.V1;
using OcrApi.Model;

namespace OcrApi.Service
{
    public class GCPVisionService
    {
        public string ExtrairDados(ImagemUpload imagemUpload)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string gcpChave = Path.Combine(currentDirectory, "SUA_CHAVE_GCP.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", gcpChave);
            var client = ImageAnnotatorClient.Create();
            byte[] imageBytes = Convert.FromBase64String(imagemUpload.base64);

            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                Image image = Image.FromStream(ms);

                var response = client.DetectText(image);

                foreach (var anotacoes in response)
                {
                    return anotacoes.Description.Replace("\n", "");
                }
            }

            return String.Empty;
        }
    }
}
