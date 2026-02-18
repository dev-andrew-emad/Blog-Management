using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogdatalayer.entities
{
    public class commentlike
    {
        public int id {  get; set; }
        public int userid {  get; set; }
        public user user { get; set; }
        public int commentid {  get; set; }
        public comment comment { get; set; }
    }
}
