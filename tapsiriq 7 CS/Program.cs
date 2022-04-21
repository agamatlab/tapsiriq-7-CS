
partial class Program
{


    static void Main(string[] args)
    {

        Admin?[]? adminList = ReadObjects<Admin>("admins.txt");
        User?[]? userList = ReadObjects<User>("users.txt");

        Post.ID = GetLastPostID(adminList);
        Notification.ID = GetLastNotfID(adminList);
        Admin.ID = GetObjectID(adminList);   
        User.ID = GetObjectID(userList);   

        Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.OutputEncoding = System.Text.Encoding.Unicode;


        while (true)
        {
            Console.Clear();
            try
            {
                sbyte choice = Convert.ToSByte(GetChoice("Do You Want To Sign IN as:", 
                    new string[] { "Admin", "User", "Exit" }));
                if (choice == 0)
                {
                    sbyte logAdminChoice = Convert.ToSByte(GetChoice("Do You Want To:", new string[] { "SIGN IN", "SIGN UP" }, true));
                    if (logAdminChoice == 0)
                    {
                        Admin? admin = SignInAdmin(adminList);
                        if (admin != null) Console.WriteLine("Signed IN");
                        else throw new ArgumentException("False Credentials Admin.");

                        AdminMenu(admin);
                    }
                    else if (logAdminChoice == 1)
                    {
                        Admin newAdmin = CreateAdmin();
                        foreach (var admin in adminList)
                            if(admin.Username == newAdmin.Username)
                                throw new ArgumentException("Username already Exists...");

                        adminList = AddElement(adminList, newAdmin);
                    }
                }
                else if (choice == 1)
                {
                    sbyte logUserChoice = Convert.ToSByte(GetChoice("Do You Want To:", new string[] { "SIGN IN", "SIGN UP" }, true));

                    if (logUserChoice == 0)
                    {
                        User? user = SignInUser(userList);
                        if (user != null) Console.WriteLine("Signed IN");
                        else throw new ArgumentException("False Credentials User.");

                        UserMenu(user, adminList);
                    }
                    else if (logUserChoice == 1)
                    {
                        User newUser = CreateUser();
                        foreach (var user in userList)
                            if (user?.EMail == newUser.EMail) 
                                throw new ArgumentException("Email already Exists...");

                        userList = AddElement(userList, newUser);
                    }

                }
                else if(choice == 2)
                {
                    SaveChanges(adminList, "admins.txt");
                    SaveChanges(userList, "users.txt");
                    Environment.Exit(777);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}