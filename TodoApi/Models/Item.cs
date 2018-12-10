using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Item : IItem
	{
        public Boolean IsComplete
        {
            get;
            set;
        }

        public Int32 ID
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }
    }
}
