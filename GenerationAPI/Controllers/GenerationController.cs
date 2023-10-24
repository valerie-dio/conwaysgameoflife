using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GenerationAPI.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("api/[controller]/[action]")]
    public class GenerationController : ControllerBase
    {
        // Gets the current generation
        // api/Generation/Get
        [HttpGet]
        public string Get()
        {
            Generation.Generate();
            List<Cells> cells = Generation.ConvertToList(Generation.CurrentGenerationCells);
            return JsonSerializer.Serialize(cells);
        }

        // Generates random initial generation and gets the current generation
        // api/Generation/GetInitialState
        [HttpGet]
        public string GetInitialState()
        {
            Generation.GenerateInitialCells();
            List<Cells> cells = Generation.ConvertToList(Generation.CurrentGenerationCells);
            return JsonSerializer.Serialize(cells);
        }

        // Gets the pre-defined dimension of the game
        // api/Generation/GetDimension
        [HttpGet]
        public string GetDimension()
        {
            int[] dim = { Generation.HEIGHT, Generation.WIDTH };
            return JsonSerializer.Serialize(dim);
        }
    }
}