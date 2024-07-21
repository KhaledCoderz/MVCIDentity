using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCIDentity.Controllers.Base;
using MVCIDentity.Core.Extensions;
using MVCIDentity.Core.IServices;
using MVCIDentity.Models.Entity;
using MVCIDentity.Models.ViewModel.Car;

namespace MVCIDentity.Controllers
{
    [Authorize(Roles ="User")]
    public class CarsController : BaseController
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _carService.GetCarByUserId(CurrentUser.Id));
        }

        //// GET: Cars/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null || _context.Car == null)
        //    {
        //        return NotFound();
        //    }

        //    Car car = await _context.Car
        //        .Include(c => c.Identity)
        //        .FirstAsync(m => m.Id == id);
        //    if (car.IsNotNullOrEmpty())
        //    {
        //        return View(car);

        //    }
        //    return NotFound();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageSave(CarSaveModel SaveModel)
        {
            if (ModelState.IsValid)
            {
                var car = new Car
                {
                    Id = SaveModel.Id,
                    Type = SaveModel.Type,
                    UserId = SaveModel.UserId,
                };

                await _carService.SaveCar(car);
                return RedirectToAction("Index");
            }
            var manageCarModel = await _carService.GetCarManageModel((long)SaveModel.Id);

            manageCarModel.Type = SaveModel.Type;

            if (SaveModel.UserId.IsNotNullOrEmpty())
            {
                (manageCarModel.UsersSelectList.FirstOrDefault(x => x.Selected == true) ?? new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()).Selected = false;
                manageCarModel.UsersSelectList.Find(x => x.Value == SaveModel.UserId)!.Selected = true;
            }
            
            
            return View("Manage", manageCarModel);
        }

        public async Task<IActionResult> Manage(long? id = 0)
        {
            if (id.IsNotNullOrEmpty())
            {
                var car = await _carService.GetCarManageModel((long)id);
                return View(car);
            }
            else
            {
                return NotFound();
            }
        }



       

    }
}
