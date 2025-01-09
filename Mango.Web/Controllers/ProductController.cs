using Mango.Web.Models.Product;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public ProductController(IProductService productService, ILogger<ProductController> logger, JsonSerializerOptions jsonOptions)
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

        public IActionResult Create()
        {
            var categoryList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Electronics", Value = "Electronics" },
                new SelectListItem { Text = "Clothes", Value = "Clothes" },
                new SelectListItem { Text = "Groceries", Value = "Groceries" },
                new SelectListItem { Text = "Books", Value = "Books" },
                new SelectListItem { Text = "Books", Value = "Books" },
                new SelectListItem { Text = "Other", Value = "Other" }
            };

            ViewBag.CategoryList = categoryList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _productService.CreateProductsAsync(productDto);

                    if (response is { IsSuccess: true })
                    {
                        TempData["success"] = "Product has been created";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["error"] = response?.Message;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating product.");
                    TempData["error"] = "An error occurred while creating the product.";
                }
            }

            return View(productDto);
        }

        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                var categoryList = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Electronics", Value = "Electronics" },
                    new SelectListItem { Text = "Clothes", Value = "Clothes" },
                    new SelectListItem { Text = "Groceries", Value = "Groceries" },
                    new SelectListItem { Text = "Books", Value = "Books" },
                    new SelectListItem { Text = "Books", Value = "Books" },
                    new SelectListItem { Text = "Other", Value = "Other" }
                };

                ViewBag.CategoryList = categoryList;

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

        [HttpPost]
        public async Task<IActionResult> Delete(ProductDto productDto)
        {
            try
            {
                var response = await _productService.DeleteProductsAsync(productDto.Id);

                if (response is { IsSuccess: true })
                {
                    TempData["success"] = "Product has been deleted";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product.");
                TempData["error"] = "An error occurred while deleting the product.";
            }

            return View(productDto);
        }

        public async Task<IActionResult> Edit(int productId)
        {
            try
            {
                var categoryList = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Electronics", Value = "Electronics" },
                    new SelectListItem { Text = "Clothes", Value = "Clothes" },
                    new SelectListItem { Text = "Groceries", Value = "Groceries" },
                    new SelectListItem { Text = "Books", Value = "Books" },
                    new SelectListItem { Text = "Books", Value = "Books" },
                    new SelectListItem { Text = "Other", Value = "Other" }
                };

                ViewBag.CategoryList = categoryList;

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

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _productService.UpdateProductsAsync(productDto);

                    if (response is { IsSuccess: true })
                    {
                        TempData["success"] = "Product has been updated";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["error"] = response?.Message;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating product.");
                    TempData["error"] = "An error occurred while creating the product.";
                }
            }

            return View(productDto);
        }
    }
}
