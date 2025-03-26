namespace inSession.Controllers
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
        public int UserId { get; set; }
    }
}