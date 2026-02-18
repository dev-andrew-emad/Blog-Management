namespace blogbusinesslayer.dtos
{
    public class postdto
    {
        public int id {  get; set; }
        public string title {  get; set; }
        public string content {  get; set; }
        public string authorname {  get; set; }
        public DateTime createdat {  get; set; }
        public bool ispublished {  get; set; }
        public List<commentwithusername> comments { get; set; }
        public int postlikes {  get; set; }
    }
}
