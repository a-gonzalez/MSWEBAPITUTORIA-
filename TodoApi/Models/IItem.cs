using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    interface IItem
    {
		Int32 ID
		{
			get;
			set;
		}

		String Name
		{
			get;
			set;
		}

		Boolean IsComplete
		{
			get;
			set;
		}
    }
}
