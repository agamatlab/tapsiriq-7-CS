
/*
    Admin=>id,username,email,password,Posts,Notifications
    User=>id,name,surname,age,email,password
    Post=>id,Content,CreationDateTime,LikeCount,ViewCount
*/


class User : Person
{
    public static int ID = 0;
    private string _email = "";
    public override string EMail
    {
        get => _email;
        set
        {
            if (value.Contains('@')) _email = value;
            else throw new ArgumentException("Email Must Contain '@'.");
        }
    }

    private string _password = "";
    public override string Password
    {
        get => _password;
        set
        {
            if (value.Length > 7) _password = value;
            else throw new ArgumentException("Password must be Longer than 7 Characters.");

        }
    }
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public int[]? LikedPosts { get; set; } = null;

    public User() {
        ObjectID = ID++;
    }
    public User(string mail, string password)
    {
        ObjectID = ID++;
        EMail = mail;
        Password = password;
    }
    public User(string eMail, string password, string name, string surname, int[]? likedPosts)
    {
        ObjectID = ID++;
        LikedPosts = likedPosts;
        EMail = eMail;
        Password = password;
        Name = name;
        Surname = surname;
    }

    public static bool operator ==(User a, User b) => a?.EMail == b?.EMail && a?.Password == b?.Password;
    public static bool operator !=(User a, User b) => a?.EMail != b?.EMail || a?.Password != b?.Password;

}

partial class Program
{
    public static void UserMenu(User user, Admin?[] adminList)
    {
        while (true)
        {
            (Post? chosenPost, Admin? chosenAdmin) = GetChoice("Posts:", adminList, true);
            if (chosenPost == null && chosenAdmin == null) break;

            Mail.Send(Mail.DefaultMail, $"{user.Name} {user.Surname} has VIEWED {chosenAdmin.Username}'s Post.", "Log Console");
            chosenAdmin.Notifications = AddElement(chosenAdmin.Notifications,
                new Notification($"{user.Name} {user.Surname} has VIEWED {chosenAdmin.Username}'s Post.", DateTime.Now, user));
            chosenPost.ViewCount++;
            while (true)
            {
                bool? userLiked = user.LikedPosts?.Contains(chosenPost.PostID);
                sbyte userChoice = Convert.ToSByte(GetChoice(chosenPost.ToString(),
                    new string[] { userLiked == false || userLiked == null ? "Like" : "UnLike", "Back" }));
                if (userChoice == 0)
                    if (userLiked == null || userLiked == false)
                    {
                        chosenPost.LikeCount++;
                        user.LikedPosts = AddElement(user.LikedPosts, chosenPost.PostID);
                        Mail.Send(Mail.DefaultMail, $"{user.Name} {user.Surname} has LIKED {chosenAdmin.Username}'s Post.", "Log Console");
                        chosenAdmin.Notifications = AddElement(chosenAdmin.Notifications,
                            new Notification($"{user.Name} {user.Surname} has LIKED {chosenAdmin.Username}'s Post.", DateTime.Now, user));
                    }
                    else
                    {
                        chosenPost.LikeCount--;
                        user.LikedPosts = user.LikedPosts.Except(new int[] { chosenPost.PostID }).ToArray();
                        Mail.Send(Mail.DefaultMail, $"{user.Name} {user.Surname} has UNLIKED {chosenAdmin.Username}'s Post.", "Log Console");
                        chosenAdmin.Notifications = AddElement(chosenAdmin.Notifications,
                            new Notification($"{user.Name} {user.Surname} has UNLIKED {chosenAdmin.Username}'s Post.", DateTime.Now, user));
                    }
                else break;
            }
        }
    }
}
