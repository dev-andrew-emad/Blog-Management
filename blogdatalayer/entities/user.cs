namespace blogdatalayer.entities
{
    public class user
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public ICollection<post>posts { get; set; }=new List<post>();
        public ICollection<comment>comments { get; set; } =new List<comment>();
        public ICollection<like> likes { get; set; }=new List<like>();
        public ICollection<commentlike> commentlikes { get; set; } = new List<commentlike>();
        public ICollection<reply> replies { get; set; } = new List<reply>();
        public ICollection<replylike> replylikes { get; set; } = new List<replylike>();
    }
}
