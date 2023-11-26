namespace Itogi.Entities;

public class Post
{
    public Post(int postId, string postName)
    {
        PostId = postId;
        PostName = postName;
    }

    public int PostId { get; set; }
    public string PostName { get; set; }
}