namespace blogbusinesslayer.dtos
{
    public class commentwithusername
    {
        public int id {  get; set; }
        public string authorname { get; set; }
        public string content { get; set; }
        public List<replywithusername> replies { get; set; }
        public int commentlikes {  get; set; }
    }
}
