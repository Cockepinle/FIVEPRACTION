using Salon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon
{
    internal class ModelClass : IToFindModel
    {
        public string servicesName {  get; set; }
        public float price {  get; set; }
        public int id { get; set; }
    }
}
