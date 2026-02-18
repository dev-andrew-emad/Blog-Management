using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogdatalayer.entities
{
    public class reply
    {
        public int id {  get; set; }
        public int userid {  get; set; }
        public user user { get; set; }
        public string content {  get; set; }
        public int commentid {  get; set; }
        public comment comment { get; set; }
        public ICollection<replylike> replylikes { get; set; }=new List<replylike>();
    }
}
