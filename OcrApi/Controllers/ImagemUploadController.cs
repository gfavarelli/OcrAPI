using Microsoft.AspNetCore.Mvc;
using OcrApi.Model;
using OcrApi.Service;

namespace OcrApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagemUploadController : ControllerBase
    {

        [HttpPost]
        public IActionResult Upload([FromBody] ImagemUpload uploadImagem)
        {
            Pessoa pessoa = new Pessoa();
            GCPVisionService gcpVision = new GCPVisionService();
            ChatGPTService chatGPT = new ChatGPTService();  
            string dados = gcpVision.ExtrairDados(uploadImagem);
            pessoa = chatGPT.ExtrairDados(dados);
            return Ok(pessoa);
        }
    }
}
