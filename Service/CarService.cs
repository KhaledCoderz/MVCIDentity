using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCIDentity.Core.Extensions;
using MVCIDentity.Core.IServices;
using MVCIDentity.Data;
using MVCIDentity.Models.Entity;
using MVCIDentity.Models.ViewModel.Car;

namespace MVCIDentity.Service
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarViewModel>> GetCarByUserId(string UserID)
        {
            try
            {
                var Cars = await _context.Car.Where(row => row.UserId == UserID)
               .Include(x => x.Identity)
               .ToListAsync();

                if (Cars.IsNotNullOrEmpty())
                {
                    return Cars.Select(r => new CarViewModel
                    {
                        Id = r.Id,
                        Type = r.Type,
                        UserId = r.UserId,
                        FullName =  r.Identity.FirstName + " - " + r.Identity.LastName,
                    }).ToList();
                }
                return new List<CarViewModel>();
            }
            catch (Exception)
            {
                return new List<CarViewModel>();
            }
        }


        public async Task<CarManageModel> GetCarManageModel(long id)
        {
            CarManageModel car;


            if (id != 0)
            {
                var carEntity = (await _context.Car.FindAsync(id));
                if (carEntity.IsNotNullOrEmpty())
                {
                    car = new CarManageModel
                    {
                        Id = carEntity.Id,
                        Type = carEntity.Type,
                        UserId = carEntity.UserId,
                    };
                }
                else
                {
                    car = new CarManageModel
                    {
                        Id = 0
                    };
                }
            }
            else
            {
                car = new CarManageModel {
                    Id = 0
                };
            }

            var lstUsersItems = new List<SelectListItem>();

            foreach (var item in  _context.Users)
            {
                lstUsersItems.Add(new SelectListItem(item.FirstName + " " + item.LastName,item.Id, item.Id == car.UserId));
            }

            car.UsersSelectList = lstUsersItems;

            return car; 

        }

        public async Task SaveCar(Car car)
        {
            try
            {
                if (car.Id == 0)
                {
                   var xx = await _context.Car.AddAsync(car);
                }
                else
                {
                    _context.Car.Update(car);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
            }
        }
    }

}
