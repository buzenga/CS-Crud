using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagementApp
{
    internal class CarsCollection
    {
        public static List<Car> cars { get; set; } = new List<Car>();

        public static void AddCar(Car car)
        {
            cars.Add(car);
        }

        public static void ShowCollection()
        {
            foreach (var item in cars)
            {
                Console.WriteLine(item.PlateNumber);
                Console.WriteLine(item.ProducerName);
                Console.WriteLine(item.Model);
                Console.WriteLine(item.Owner);
                Console.WriteLine("-------------------------");
            }
        }

        public static void ClearCollection()
        {
            cars.Clear();
        }
    }
}
