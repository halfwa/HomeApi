using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Data.Queries
{
    public class UpdateRoomQuery
    {
        public string Name { get; set; }
        public int Area { get; set; }
        public bool GasConnected { get; set; }
        public int Voltage { get; set; }

        public UpdateRoomQuery(
            string name = null, 
            int area = default,
            bool gasConnected = default,
            int voltage = default)
        {
            Name = name;
            Area = area;
            GasConnected = gasConnected;
            Voltage = voltage;
        }
    }
}
