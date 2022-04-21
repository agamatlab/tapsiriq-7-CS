// Notification=>id,Text,DateTime,FromUser(bu hansi user terefinden bu bildirishin geldiyidir)
class Notification
{
    public static int ID = 0;

    public Notification(string text, DateTime time, User fromUser)
    {
        ++ID;
        Text = text;
        Time = time;
        FromUser = fromUser;
    }

    public int NotfID { get; set; } = ID++;
    public string Text { get; set; }
    public DateTime Time { get; set; }
    public User? FromUser { get; set; } = null;

    public override string ToString() => $"{Text} => {Time} => {FromUser?.Name}";
}
