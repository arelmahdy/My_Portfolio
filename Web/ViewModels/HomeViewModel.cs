using Core.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class HomeViewModel
    {
        public owner Owner { get; set; }
        public List<ProtiflioItem> portfolio { get; set; }
    }
}
