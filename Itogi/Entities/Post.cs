namespace Itogi.Entities;

public class Post
{
    public Post(int postId, string postName)
    {
        Post_id = postId;
        Post_name = postName;
    }

    public int Post_id { get; set; }
    public string Post_name { get; set; }
}