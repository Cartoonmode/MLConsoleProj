using MLConsoleProj;
using MLWebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using System.Diagnostics;

namespace MLWebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PredictionEnginePool<MLModel7.ModelInput, MLModel7.ModelOutput> _predictionImage;

        public HomeController(ILogger<HomeController> logger,
            PredictionEnginePool<MLModel7.ModelInput, MLModel7.ModelOutput> predictionImage)
        {
            _logger = logger;
            _predictionImage = predictionImage;
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            //var bytes = System.IO.File.ReadAllBytes(@"D:\Defects\images_test\crease\img_01_425382900_00002.jpg");

            using MemoryStream ms = new MemoryStream();
            file.OpenReadStream().CopyTo(ms);

            var bytes = ms.ToArray();

            MLModel7.ModelInput sampleImage = new MLModel7.ModelInput()
            {
                ImageSource = bytes
            };

            var predictImage = _predictionImage.Predict(sampleImage);

            var labels = ModelLabel.GetLabels();

            for (int i = 0; i < predictImage.Score.Length; i++)
            {
                var label = labels.FirstOrDefault(x => x.Id == i);

                if (label != null) label.Score = predictImage.Score[i];
            }

            labels = labels.OrderByDescending(x => x.Score).Take(3).ToList();

            return View(labels);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(null);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}