using MVCIDentity.Models.Entity;
using MVCIDentity.Models.ViewModel.Car;

namespace MVCIDentity.Core.IServices
{
    public interface ICarService
    {
        public Task<List<CarViewModel>> GetCarByUserId(string UserID);
        Task<CarManageModel> GetCarManageModel(long id);
        Task SaveCar(Car car);
    }
}
