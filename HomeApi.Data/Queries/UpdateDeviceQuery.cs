using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Data.Queries
{
    public class UpdateDeviceQuery
    {
        public string NewName { get; set; }
        public string NewSerial { get; set; }

        public UpdateDeviceQuery(string newName = null, string newSerial = null)
        {
            NewName = newName;
            NewSerial = newSerial;
        }
    }
}
