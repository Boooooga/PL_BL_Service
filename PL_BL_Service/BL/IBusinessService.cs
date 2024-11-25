using PL_BL_Service.Models;
namespace PL_BL_Service.BL
{
    public interface IBusinessService
    {
        List<Bus> GetAllBuses();
        Bus GetBus(int id);
        bool AddBus(Bus bus);
        bool UpdateBus(int id, Bus bus);
        bool DeleteBus(int id);

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
