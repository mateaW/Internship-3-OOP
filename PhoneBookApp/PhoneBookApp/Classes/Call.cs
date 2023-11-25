using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBookApp.Enums;

namespace PhoneBookApp
{
    public class Call
    {
        public DateTime CallTime { get; set; }
        public Status Status { get; set; }

        public Call(DateTime callTime, Status status)
        {
            CallTime = callTime;
            Status = status;
        }
    }
}
