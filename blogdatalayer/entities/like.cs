namespace blogdatalayer.entities
{
    public class like
    {
        public int id { get; set; }
        public int userid { get; set; }
        public user user { get; set; }
        public int postid { get; set; }
        public post post { get; set; }
        public DateTime createdat { get; set; }
    }
}
