using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogdatalayer.entities
{
    public class replylike
    {
        public int id {  get; set; }
        public int userid {  get; set; }
        public user user { get; set; }
        public int replyid {  get; set; }
        public reply reply { get; set; }
    }
}
