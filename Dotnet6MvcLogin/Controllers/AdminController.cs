using Dotnet6MvcLogin.Models.Domain;
using Dotnet6MvcLogin.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet6MvcLogin.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        DatabaseContext _dbContext;
        UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(DatabaseContext dbContext, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Display()
        {
            IQueryable<ApplicationForm> form = _dbContext.ApplicationForms.AsQueryable();
            return View(form.ToList());
        }
        public IActionResult AddProducts()
        {
            //list out all the products here
            IQueryable<Products> form = _dbContext.Products.AsQueryable();
            return View(form.ToList());
            
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var product = new Products
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Id = viewModel.Id
                };

                if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await viewModel.ImageFile.CopyToAsync(stream);
                        product.ImageData = stream.ToArray();
                        product.ImageMimeType = viewModel.ImageFile.ContentType;
                    }
                }

                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("AddProducts");
            }

            return View(viewModel);
        }

        public IActionResult DeleteProduct(int id)
        {
            var dataToDelete = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            _dbContext.Products.Remove(dataToDelete);
            _dbContext.SaveChanges();
            return RedirectToAction("Display");
        }
        public IActionResult Details(int id, int productId)
        {
            // Retrieve the data for the specified id
            ApplicationForm form = _dbContext.ApplicationForms.FirstOrDefault(f => f.ApplicationNumber == id);
            Products product = _dbContext.Products.FirstOrDefault(f => f.Id == productId);
            var viewModel = new DetailsModel
            {
                ProductData= product,
                FormData=form
            };
            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int applicationNumber, string status)
        {
            var form = await _dbContext.ApplicationForms.FindAsync(applicationNumber);
            if (form == null)
            {
                // Handle the case when the form is not found
                return NotFound();
            }

            // Update the status
            form.Status = status;
            await _dbContext.SaveChangesAsync();

            // Redirect back to the display page
            return RedirectToAction("Display");
        }

      

       

    }
}
