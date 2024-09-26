using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HarvestFarm
{
    [DataContract]
    public class Tomato : Product, ICare
    {
        [DataMember]
        public int NumFertilizer { get; set; }
        [DataMember]
        public int NumWater { get; set; }

        public Tomato(int cost, int value, int duration, int fertilizerCost, int waterCost)
        {
            Cost = cost;
            Value = value;
            Duration = duration;
            FertilizerCost = fertilizerCost;
            WaterCost = waterCost;
        }
        public Tomato() { }
        public override void Seed()
        {
            Console.WriteLine("Seeding Tomato.");
            Start = DateTime.Now;
        }

        public override void Harvest()
        {
            while (true)
            {
                TimeSpan timeSinceSeed = DateTime.Now - Start;

                if (timeSinceSeed.TotalSeconds >= Duration)
                {
                    Console.WriteLine("Harvesting " + GetType().Name);
                    break;
                }
                else
                {
                    Console.WriteLine("Need more time to harvest.");
                    System.Threading.Thread.Sleep(2000);
                }
            }
        }

        public void Feed()
        {
            Console.WriteLine("Feeding Tomato.");
            NumFertilizer++;
        }

        public void ProvideWater()
        {
            Console.WriteLine("Watering Tomato.");
            NumWater++;
        }
    }
}
