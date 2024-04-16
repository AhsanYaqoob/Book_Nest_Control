using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shop_Managment
{

    class YourDataItems
    {
        public int BId { get; set; }
        public string BTitle { get; set; }
        public string BAuthor { get; set; }
        public string BCat { get; set; }
        public int BQty { get; set; }
        public decimal BPrice { get; set; }


        public bool Contains(string searchTerm)
        {
            return BId.ToString().Contains(searchTerm) || BTitle.Contains(searchTerm) || BAuthor.Contains(searchTerm) || BCat.Contains(searchTerm) || BQty.ToString().Contains(searchTerm) || BPrice.ToString().Contains(searchTerm);
            // Add conditions for other properties as needed
        }
    }
   
}
