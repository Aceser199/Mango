using Mango.Web.Models;
using Mango.Web.Models.Product;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public HomeController(IProductService productService, ILogger<ProductController> logger, JsonSerializerOptions jsonOptions)
        {
            _productService = productService;
            _logger = logger;
            _jsonOptions = jsonOptions ?? throw new ArgumentNullException(nameof(jsonOptions));
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto>? list = new();

            try
            {
                var response = await _productService.GetAllProductsAsync();

                if (response is { IsSuccess: true } && response.Result != null)
                {
                    list = JsonSerializer.Deserialize<List<ProductDto>>(response.Result.ToString()!, _jsonOptions);
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching products.");
                TempData["error"] = "An error occurred while fetching products.";
            }

            return View(list);
        }

        public async Task<IActionResult> Details(int productId)
        {
            try
            {
                var response = await _productService.GetProductByIdAsync(productId);

                if (response is { IsSuccess: true } && response.Result != null)
                {
                    var productDto = JsonSerializer.Deserialize<ProductDto>(response.Result.ToString()!, _jsonOptions);
                    return View(productDto);
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching product for deletion.");
                TempData["error"] = "An error occurred while fetching the product.";
            }

            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
