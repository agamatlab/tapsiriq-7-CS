
/*
    Admin=>id,username,email,password,Posts,Notifications
    User=>id,name,surname,age,email,password
    Post=>id,Content,CreationDateTime,LikeCount,ViewCount
*/

abstract class Person
{
    public int ObjectID { get; set; }
    public abstract string EMail { get; set; }
    public abstract string Password { get; set; }
}
