using blogdatalayer.entities;

namespace blogdatalayer.entities
{
    public class post
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int authorid { get; set; }
        public user author { get; set; }
        public DateTime createdat { get; set; }
        public bool ispublished { get; set; }
        public ICollection<comment> comments { get; set; } = new List<comment>();
        public ICollection<like> likes { get; set; } = new List<like>();
    }
}
