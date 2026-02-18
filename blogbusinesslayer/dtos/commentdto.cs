namespace blogbusinesslayer.dtos
{
    public class commentdto
    {
        public int id { get; set; }
        public string postcontent { get; set; }

        public string commentcontent { get; set; }
        public List<string> replies { get; set; }
        public int commentlikes {  get; set; }
        public DateTime createdat { get; set; }

    }
}
