
/*
    Admin=>id,username,email,password,Posts,Notifications
    User=>id,name,surname,age,email,password
    Post=>id,Content,CreationDateTime,LikeCount,ViewCount
*/

class Post
{
    public static int ID = 0;

    public Post(string content, DateTime creationDateTime, uint likeCount = 0, uint viewCount = 0)
    {
        Content = content;
        CreationDateTime = creationDateTime;
        LikeCount = likeCount;
        ViewCount = viewCount;
    }

    public int PostID { get; set; } = ID++;
    public string Content { get; set; }
    public DateTime CreationDateTime{ get; set; }
    public uint LikeCount { get; set; } = 0;
    public uint ViewCount { get; set; } = 0;

    public string ShowShortInfo() => (Content.Length <= 5) ? Content : (Content.Substring(0, 5) + "...") + $" => {CreationDateTime.ToShortDateString()}";
    
    public override string ToString() => $"{Content}... => {CreationDateTime.ToString()}" +
        $"\n\t=> Like Count: {LikeCount} => View Count: {ViewCount}";
}
