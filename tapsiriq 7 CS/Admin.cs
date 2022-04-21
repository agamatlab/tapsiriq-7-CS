class Admin : Person
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
    public override string Password { 
        get => _password; 
        set { 
            if (value.Length > 7) _password = value; 
            else throw new ArgumentException("Password must be Longer than 7 Characters.");
        
        } 
    }
    public string Username { get; set; }
    public Post?[]? Posts { get; set; } = null;
    public Notification?[]? Notifications { get; set; } = null;

    public void CreatePost()
    {
        string content = Console.ReadLine() ?? "";
        Post post = new Post(content, DateTime.Now);
        Posts = (Posts != null)
            ? Posts.Append(post).ToArray()
            : new Post?[] { post };
    }

    public void ShowNotifications()
    {
        if (Notifications == null || Notifications.Length == 0) { Console.WriteLine("No Notification Exists..."); return; }
        foreach (var notfication in Notifications)
            Console.WriteLine(notfication);
    }

    public void ShowPosts()
    {
        if (Posts == null || Posts.Length == 0) {Console.WriteLine("No Post Exists..."); return; }
        foreach (var post in Posts)
            Console.WriteLine(post);
    }

    public Admin() {
        ObjectID = ID++;
    }
    public Admin(string username,string password) {
        ObjectID = ID++;
        Username = username;
        Password = password;
    }
    public Admin(string eMail, string password, string username, Post?[] posts, Notification?[] notifications)
    {
        ObjectID = ID++;
        EMail = eMail;
        Password = password;
        Username = username;
        Posts = posts;
        Notifications = notifications;
    }

    public static bool operator==(Admin a, Admin b) => a?.Username == b?.Username && a?.Password == b?.Password;
    public static bool operator!=(Admin a, Admin b) => a?.Username != b?.Username || a?.Password != b?.Password;

    public override bool Equals(object? obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();
}


partial class Program
{
    public static void AdminMenu(Admin admin)
    {
        while (true)
        {
            sbyte adminChoice = Convert.ToSByte(GetChoice("Do You Want To:",
                new string[] { "Show All Posts", "Show All Notifications", "Create Post" }, true));

            if (adminChoice == 0) admin.ShowPosts();
            else if (adminChoice == 1) admin.ShowNotifications();
            else if (adminChoice == 2) admin.CreatePost();
            else if (adminChoice == -1) break;
            Console.ReadKey();
        }
    }

    public static Admin CreateAdmin()
    {
        Admin inAdmin = new Admin();

        Console.Write("Enter Username: ");
        inAdmin.Username = Console.ReadLine() ?? "";

        Console.Write("Enter Password: ");
        inAdmin.Password = Console.ReadLine() ?? "";

        Console.Write("Enter Email: ");
        inAdmin.EMail = Console.ReadLine() ?? "";

        return inAdmin;
        // File.AppendAllText("admins.txt", JsonSerializer.Serialize(inAdmin) + '\n');
    }
}
