using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogbusinesslayer.dtos
{
    public class replydto
    {
        public int id {  get; set; }
        public string post {  get; set; }
        public string comment {  get; set; }
        public string reply {  get; set; }
        public int replylikes {  get; set; }
    }
}
