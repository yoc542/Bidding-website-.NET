using Dotnet6MvcLogin.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dotnet6MvcLogin.Models.DTO;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using Microsoft.EntityFrameworkCore;

namespace Dotnet6MvcLogin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        DatabaseContext _dbContext;
        UserManager<ApplicationUser> _userManager; 
        public DashboardController(DatabaseContext dbContext, UserManager<ApplicationUser> userManager)
        {
           this._dbContext = dbContext;
           this._userManager = userManager;
        }

        public IActionResult Bid(int id)
        {
            // Find the product by its ID
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); // Handle the case when the product is not found
            }

            // Create a new ApplicationForm and populate it with product details
            var applicationForm = new ApplicationForm
            {
                productId = product.Id,
                // Adjust this property based on your product model
            };

            return View(applicationForm);
        }

        [HttpPost]
        public async Task<IActionResult> Bid(ApplicationForm form)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            form.SubmittedDate = DateTime.Now;
            form.UserId = currentUser.Id;
            form.FirstName=currentUser.FirstName;
            form.LastName=currentUser.LastName;
            form.Status = "Pending"; // Set the initial status as "Pending" for newly created forms

            _dbContext.ApplicationForms.Add(form);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Display");
        }

        public IActionResult ViewProducts() 
        {
            IQueryable<Products> form = _dbContext.Products.AsQueryable();
            return View(form);
        }
    
        public async Task<IActionResult> Display()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Display", "Admin");
            }    
            else
            {
                var currentUser = await _userManager.GetUserAsync(User);

                // Retrieve the application forms created by the current user
                var userForms = _dbContext.ApplicationForms
                    .Where(f => f.UserId == currentUser.Id)
                    .ToList();

                return View(userForms);
            }
        }

     

        [HttpPost]
        

        public IActionResult Delete(int id)  
        {
            var dataToDelete = _dbContext.ApplicationForms.FirstOrDefault(x => x.ApplicationNumber == id);
            _dbContext.ApplicationForms.Remove(dataToDelete);
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
                ProductData = product,
                FormData = form
            };
            return View(viewModel);

        }
    }
}
