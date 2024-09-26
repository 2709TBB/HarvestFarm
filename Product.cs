using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HarvestFarm
{
    [JsonDerivedType(typeof(Tomato), "tomato")]
    [JsonDerivedType(typeof(Wheat), "wheat")]
    [JsonDerivedType(typeof(Sunflower), "sunflower")]

    [XmlInclude(typeof(Sunflower))]
    [XmlInclude(typeof(Wheat))]
    [XmlInclude(typeof(Tomato))]

    [KnownType(typeof(Wheat))]
    [KnownType(typeof(Tomato))]
    [KnownType(typeof(Sunflower))]
    [DataContract]
    public abstract class Product
    {
        [DataMember]
        public int Cost { get; set; }
        [DataMember]
        public int Value { get; set; }
        [DataMember]
        public DateTime Start { get; set; }
        [DataMember]
        public int Duration { get; set; }
        [DataMember]
        public int FertilizerCost { get; set; }
        [DataMember]
        public int WaterCost { get; set; }

        
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
