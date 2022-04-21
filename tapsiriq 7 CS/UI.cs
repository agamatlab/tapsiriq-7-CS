partial class Program
{
    public static void MySetColor(ConsoleColor foreground, ConsoleColor background)
    {
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;
    }

    public static int ManageChoice(ref ushort choice, ushort answerCount, bool IsEscape)
    {
        // Uİ'da dəyərlərin keyboardla idarəsini təmin edir.

        ConsoleKey kb = Console.ReadKey().Key;

        switch (kb)
        {
            case ConsoleKey.W:
            case ConsoleKey.A:
            case ConsoleKey.UpArrow:
            case ConsoleKey.LeftArrow:
                if (choice == 0) choice = answerCount;
                choice--;
                return 1;
            case ConsoleKey.S:
            case ConsoleKey.D:
            case ConsoleKey.DownArrow:
            case ConsoleKey.RightArrow:
                choice++;
                choice %= answerCount;
                return 1;
            case ConsoleKey.Enter:
                return 0;
            case ConsoleKey.Escape:
                if(IsEscape)return -1;
                return 1;
            default:
                return 1;
        }
    }

    public static int GetChoice(string question, string[] answers, bool IsEscape = false)
    {
        // KEYBOARD - CONTROLLED Menu ilə MOD Seçimi

        ushort answerCount = Convert.ToUInt16(answers.Length);
        sbyte notFound = 1;
        ushort choice = 0;

        while (notFound > 0)
        {
            Console.Clear();
            Console.WriteLine(question);
            for (ushort i = 0; i < answerCount; i++)
            {
                char prefix = ' ';
                if (i == choice) { MySetColor(ConsoleColor.DarkGreen, ConsoleColor.Gray); prefix = '◙'; }
                Console.WriteLine($" {prefix} << {answers[i]} >>");
                Console.ResetColor();
            }
            notFound = Convert.ToSByte(ManageChoice(ref choice, answerCount, IsEscape));
            if(notFound == -1) return notFound;
        }

        return choice;
    }


    public static (Post?, Admin?) GetChoice(string question, Admin[] admins, bool IsEscape = false)
    {
        // KEYBOARD - CONTROLLED Menu ilə MOD Seçimi
        if (admins == null) { Console.WriteLine("There is no Posts to Show..."); return (null,null); }

        ushort answerCount = default;
        foreach (Admin admin in admins)
            answerCount += Convert.ToUInt16(admin.Posts?.Length);

        sbyte notFound = 1;
        ushort choice = 0;

        Post? chosenPost = null;
        Admin? chosenAdmin = null;

        while (notFound > 0)
        {
            Console.Clear();
            Console.WriteLine(question);
            ushort i = default;
            foreach(var admin in admins)
                if(admin.Posts != null) foreach(var post in admin.Posts)
                {
                    char prefix = ' ';
                    if (i++ == choice) { MySetColor(ConsoleColor.DarkGreen, ConsoleColor.Gray); prefix = '◙'; chosenAdmin = admin;chosenPost = post; }
                    Console.WriteLine($" {prefix} << {post?.ShowShortInfo()} >>");
                    Console.ResetColor();
                }
            notFound = Convert.ToSByte(ManageChoice(ref choice, answerCount, IsEscape));
            if (notFound == -1) return (null,null);
        }

        return (chosenPost, chosenAdmin);
    }
}