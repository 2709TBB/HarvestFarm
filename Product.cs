using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HarvestFarm
{
    public abstract class Product
    {
        public int Cost { get; set; }
        public int Value { get; set; }
        public DateTime Start { get; set; }
        public int Duration { get; set; }
        public int FertilizerCost { get; set; }
        public int WaterCost { get; set; }

        [JsonConstructor]
        public Product()
        {

        }
        public int TotalCost
        {
            get { return Cost + FertilizerCost + WaterCost; }
        }

        public abstract void Seed();
        public abstract void Harvest();
    }
}
