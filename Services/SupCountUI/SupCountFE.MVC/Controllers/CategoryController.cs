using AutoMapper;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Category;

namespace SupCountFE.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categories = await _categoryService.GetAllAsync();
            return View (_mapper.Map<List<CategoryVM>>(categories));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCategoryVM());
        }

        [HttpPost]
     
        public async Task<IActionResult> Create(CreateCategoryVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _categoryService.CreateAsync(model);
            if (result != null)
            {
                TempData["Success"] = "Category created successfully!";
                return RedirectToAction("List");
            }

            ModelState.AddModelError("", "Failed to create category.");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            var model = new UpdateCategoryVM
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCategoryVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _categoryService.UpdateAsync(model);
            TempData["Success"] = "Category updated successfully!";
            return RedirectToAction("List");
        }

    }
}
