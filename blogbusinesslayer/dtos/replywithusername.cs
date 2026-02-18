using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogbusinesslayer.dtos
{
    public class replywithusername
    {
        public int id {  get; set; }
        public string authorname { get; set; }
        public string content {  get; set; }
        public int replylikes {  get; set; }
    }
}
