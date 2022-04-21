
partial class Program
{

    public static int GetObjectID(Person?[]? people)
    {
        if (people == null) return 0;
        int lastObjectID = -1;
        foreach (var person in people)
            if (person?.ObjectID > lastObjectID) lastObjectID = person.ObjectID;
        return lastObjectID + 1;
    }

    public static int GetLastPostID(Admin?[]? admins)
    {
        if (admins == null) return 0;
        int lastPostID = -1;
        foreach (var admin in admins)
            if (admin?.Posts != null) foreach (var post in admin.Posts)
                    if (post?.PostID > lastPostID) lastPostID = post.PostID;
        return lastPostID + 1;
    }

    public static int GetLastNotfID(Admin?[]? admins)
    {
        if (admins == null) return 0;
        int lastNotfID = -1;
        foreach (var admin in admins)
            if (admin?.Notifications != null) foreach (var notf in admin.Notifications)
                    if (notf?.NotfID > lastNotfID) lastNotfID = notf.NotfID;
        return lastNotfID + 1;
    }

    public static T?[]? AddElement<T>(T?[]? arr, T element)
    {
        if (arr == null) arr = new T[] { element };
        else arr = arr.Append(element).ToArray();

        return arr;
    } 
}