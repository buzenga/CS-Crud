using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagementApp
{
    internal class Car
    {
        public  string PlateNumber { get; private set; }
        public string ProducerName { get; private set; }
        public string Model { get; private set; }
        public string Owner { get; private set; }

        public Car(string plateNumber, string producerName, string model, string owner)
        {
            PlateNumber = plateNumber;
            ProducerName = producerName;
            Model = model;
            Owner = owner;
        }

        public override string ToString()
        {
            return $"Plate number: {PlateNumber}\n" +
                   $"Producer name: {ProducerName}\n" +
                   $"Model: {Model}\n" +
                   $"Owner: {Owner}\n";



        }
    }
}
