using PL_BL_Service.Models;
namespace PL_BL_Service.BL
{
    public interface IBusinessService
    {
        Task<List<Bus>> GetAllBuses();
        Task<Bus> GetBus(int id);
        Task<bool> AddBus(Bus bus);
        Task<bool> UpdateBus(int id, Bus bus);
        Task<bool> DeleteBus(int id);

        //List<Driver> GetAllDrivers();
        //Task<string> GetDriverAsync(int id);
        //Task<string> AddDriverAsync(string jsonValue);
        //Task<string> UpdateDriverAsync(int id, string jsonValue);
        //Task<string> RemoveDriverAsync(int id);

        //Task<string> GetRoutesAsync();
        //Task<string> GetRouteAsync(int id);
        //Task<string> AddRouteAsync(string jsonValue);
        //Task<string> UpdateRouteAsync(int id, string jsonValue);
        //Task<string> RemoveRouteAsync(int id);
    }
}
