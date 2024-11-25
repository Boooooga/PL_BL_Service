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
                Thread.Sleep(200);
                string response = _rabbitMqClient.ReceiveMessage();

                if (string.IsNullOrEmpty(response))
                {
                    return buses;
                }

                foreach (string json in response.Split('\n'))
                {
                    if (json != "")
                        buses.Add(JsonConvert.DeserializeObject<Bus>(json));
                }

                return buses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении автобусов: {ex.Message}");
                return new List<Bus>();
            }
        }
        public Bus GetBus(int id)
        {
            try
            {
                Bus bus;
                _rabbitMqClient.SendMessage($"Buses Get {id}");
                Thread.Sleep(200);
                string response = _rabbitMqClient.ReceiveMessage();

                if (string.IsNullOrEmpty(response))
                {
                    return null;
                }

                response = response.Replace("\n", "");
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
                Thread.Sleep(200);
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
        public bool UpdateBus(int id, Bus bus)
        {
            try
            {
                _rabbitMqClient.SendMessage($"Buses Update {id} {JsonConvert.SerializeObject(bus)}");
                Thread.Sleep(200);
                string response = _rabbitMqClient.ReceiveMessage();

                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine("Не удалось обновить автобус");
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

        public bool DeleteBus(int id)
        {
            _rabbitMqClient.SendMessage($"Buses Delete {id}");
            Thread.Sleep(200);
            string response = _rabbitMqClient.ReceiveMessage();

            if (string.IsNullOrEmpty(response))
            {
                Console.WriteLine("Не удалось удалить автобус");
                return false;
            }
            return true;
        }

        #endregion

        #region Водители
        
        #endregion

        #region Маршруты
        #endregion

    }
}
