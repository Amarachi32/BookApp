using BussinessLogic.Interfaces;
using BussinessLogic.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly ILibrarianServices _librarianServices;
        private readonly ICatalogueServices _catalogueServices;
        private readonly AddUpdateBookVM _addUpdateBookVM;

        public BookController(ILibrarianServices librarianServices, ICatalogueServices catalogueServices, AddUpdateBookVM addUpdateBookVM)
        {
            _librarianServices = librarianServices;
            _catalogueServices = catalogueServices;
            _addUpdateBookVM = addUpdateBookVM;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _librarianServices.GetAuthorsWithBooksAsync();

            return View(model);
        }
        public IActionResult New()
        {
            return View(_addUpdateBookVM);
        }

        [HttpPost]
        public async Task<IActionResult> Save(AddUpdateBookVM model)
        {
            if (ModelState.IsValid)
            {
                var (success, msg) = await _catalogueServices.AddorUpdateAsync(model);

                if (success)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index");
                }

                // TempData["ErrMsg"] = msg; 

                ViewBag.ErrMsg = msg;

                return View("New");

            }

            return View("New");

        }

        /*        public ActionResult Index1()
                {
                    var userId = User.Identity.GetUserId();
                    var books = db.Books.Where(b => b.UserId == userId).ToList();
                    return View(books);
                }*/

        public IActionResult Update()
        {
           return View(_addUpdateBookVM);
            
        }

        [HttpPost]
        public async Task<IActionResult> Update( AddUpdateBookVM model, int Id)
        {
            if (ModelState.IsValid)
            {
                var (success, msg) = await _catalogueServices.UpdateAsync(model, Id);

                if (success)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index");
                }

                // TempData["ErrMsg"] = msg; for both views and redirect to actions

                ViewBag.ErrMsg = msg;

                return View("Update");

            }

            return View("Update");

        }

        public async Task<IActionResult> Delete( int BookId)
        {
            var (success, msg) = await _catalogueServices.DeleteAsync( BookId);

            if (success)
            {
                TempData["SuccessMsg"] = msg;
                return RedirectToAction("Index");
            }

            TempData["ErrMsg"] = msg;
            return RedirectToAction("Index");
        }

    }
}
