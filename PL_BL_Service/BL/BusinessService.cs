using PL_BL_Service.Models;
using Newtonsoft.Json;

namespace PL_BL_Service.BL
{
    public class BusinessService : IBusinessService
    {
        private readonly RabbitMqClientService _rabbitMqClient;
        public BusinessService(RabbitMqClientService rabbitMqClient)
        {
            _rabbitMqClient = rabbitMqClient;
        }
        #region Автобусы
        public List<Bus> GetAllBuses()
        {
            try
            {
                List<Bus> buses = new List<Bus>();
                _rabbitMqClient.SendMessage("Buses GetAll");
                string response = _rabbitMqClient.ReceiveMessage();

                if (string.IsNullOrEmpty(response))
                {
                    return null;
                }

                foreach (string json in response.Split('\n'))
                {
                    if (json != "") // последняя строка получается пустой и генерирует объект null
                        buses.Add(JsonConvert.DeserializeObject<Bus>(json));
                }
                return buses;
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Console.WriteLine($"Ошибка при получении автобусов: {ex.Message}");
                return null;
            }
        }
        public Bus GetBus(int id)
        {
            try
            {
                Bus bus;
                _rabbitMqClient.SendMessage($"Buses Get {id}");
                string response = _rabbitMqClient.ReceiveMessage();

                if (string.IsNullOrEmpty(response))
                {
                    return null;
                }

                response.Replace("\n", "");
                bus = JsonConvert.DeserializeObject<Bus>(response);

                return bus;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении автобуса: {ex.Message}");
                return null;
            }
        }
        public bool AddBus(Bus bus)
        {
            try
            {
                _rabbitMqClient.SendMessage($"Buses Add {JsonConvert.SerializeObject(bus)}");
                string response = _rabbitMqClient.ReceiveMessage();

                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine("Не удалось добавить автобус");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении автобуса: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Водители
        
        #endregion

        #region Маршруты
        #endregion

    }
}
