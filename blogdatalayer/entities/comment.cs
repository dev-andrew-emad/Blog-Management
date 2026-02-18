using blogdatalayer.entities;

namespace blogdatalayer.entities
{
    public class comment
    {
        public int id { get; set; }
        public string content { get; set; }
        public int userid {  get; set; }
        public user user { get; set; }
        public int postid { get; set; }
        public post post { get; set; }
        public DateTime createdat { get; set; }
        public ICollection<commentlike> commentlikes { get; set; } = new List<commentlike>();
        public ICollection<reply> replies { get; set; }=new List<reply>();
    }
}
