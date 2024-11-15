using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace CustomerOrderViewer2.Models
{
    class ItemModel
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
