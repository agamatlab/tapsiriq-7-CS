using System.Text.Json;

partial class Program
{
    public static void SaveChanges<T>(T[] objects, string filename)
    {
        File.WriteAllText(filename, JsonSerializer.Serialize(objects) + '\n');
    }

    public static T[]? ReadObjects<T>(string filename)
    {
        try
        {
            return JsonSerializer.Deserialize<T[]>(File.ReadAllText(filename));
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static Admin? SignInAdmin(Admin?[] adminList)
    {
        if (adminList == null) return null;
        Admin? inAdmin;

        string?[] inputs = new string[2];
        Console.Write("Enter Username: ");
        inputs[0] = Console.ReadLine();
        Console.Write("Enter Password: ");
        inputs[1] = Console.ReadLine();
        if (adminList == null) return null;

        inAdmin = new Admin(inputs[0], inputs[1]);

        foreach (var admin in adminList)
            if (admin == inAdmin) return admin;
        return null;

    }


    public static User? SignInUser(User?[] userList)
    {

        User? inUser = new User();

        Console.Write("Enter Email: ");
        inUser.EMail = Console.ReadLine() ?? "";
        Console.Write("Enter Password: ");
        inUser.Password = Console.ReadLine() ?? "";
        if (userList == null) return null;

        foreach (var user in userList)
            if (user == inUser) return user;
        return null;

    }

}