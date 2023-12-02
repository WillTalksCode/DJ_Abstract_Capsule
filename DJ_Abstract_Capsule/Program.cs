// See https://aka.ms/new-console-template for more information
using DJ_Abstract_Capsule.Objects;

Console.WriteLine("Hello, World!");

DJ McFly = new DJ(true);
//Console.Write(McFly.GetCrateList());
McFly.DJName = "DJ McFly";
McFly.ContactPhone = "(xxx)123-4567";
McFly.ContactEmail = "thisisnot@arealemail.com";
McFly.HourlyRate = 100;
double TotalHours = 0;

bool KeepDoingGigs = true;

Console.WriteLine("It's time for " + McFly.DJName + " on the wheels of steel!");
Console.WriteLine("My hourly rate is " + McFly.HourlyRate);
Console.WriteLine("you can reach me via email " + McFly.ContactEmail + " or phone number " + McFly.ContactPhone);
Console.WriteLine("Now name your requests!");

while (KeepDoingGigs)
{
    Console.WriteLine("Select a genre");
    Console.WriteLine("1. Anything is cool!");
    Console.WriteLine("2. Hip Hop!");
    Console.WriteLine("3. Some dirty south hits!");
    Console.WriteLine("4. Some slow jams!");
    Console.WriteLine("5. Let's Rock Out!");
    List<string> choices = new List<string>() { "1", "2", "3", "4", "5" };
    string genreSelect = Console.ReadLine();
    if (!choices.Contains(genreSelect))
    {
        while (!choices.Contains(genreSelect))
        {
            Console.WriteLine("Come on man!");
            Console.WriteLine("1. Anything is cool!");
            Console.WriteLine("2. Hip Hop!");
            Console.WriteLine("3. Some dirty south hits!");
            Console.WriteLine("4. Some slow jams!");
            Console.WriteLine("5. Let's Rock Out!");
            genreSelect = Console.ReadLine();
        }
    }

    List<string> Genres = new List<string>();
    switch (genreSelect)
    {
        case "2":
            Genres = new List<string> { "Hip-Hop" };
            break;
        case "3":
            Genres = new List<string> { "Dirty South" };
            break;
        case "4":
            Genres = new List<string> { "R&B", "Soul", "Jazz" };
            break;
        case "5":
            Genres = new List<string> { "Rock" };
            break;
        default:
            break;

    }


    Console.WriteLine("Classic or modern?");
    Console.WriteLine("0. Anything is cool!");
    Console.WriteLine("1. Classic 70s");
    Console.WriteLine("2. The 80s!");
    Console.WriteLine("3. The 90s!");
    Console.WriteLine("4. keep it old school!");
    Console.WriteLine("5. 2000s!");
    Console.WriteLine("6. Modern!");

    choices = new List<string>() { "0", "1", "2", "3", "4", "5", "6" };
    string decadeSelect = Console.ReadLine();
    if (!choices.Contains(decadeSelect))
    {
        while (!choices.Contains(decadeSelect))
        {
            Console.WriteLine("Come on man, Classic or modern?");
            Console.WriteLine("0. Anything is cool!");
            Console.WriteLine("1. Classic 70s");
            Console.WriteLine("2. The 80s!");
            Console.WriteLine("3. The 90s!");
            Console.WriteLine("4. keep it old school!");
            Console.WriteLine("5. 2000s!");
            Console.WriteLine("6. Modern!");
            decadeSelect = Console.ReadLine();
        }
    }

    int decadeSelectToInt = Int32.Parse(decadeSelect);


    Console.WriteLine("Explicit Content?");
    Console.WriteLine("1. Keep it clean for the kids!");
    Console.WriteLine("2. Nah, I don't care.");


    choices = new List<string>() { "1", "2" };
    string explicitContentSelect = Console.ReadLine();
    if (!choices.Contains(explicitContentSelect))
    {
        while (!choices.Contains(explicitContentSelect))
        {
            Console.WriteLine("Come on man!");
            Console.WriteLine("1. Keep it clean for the kids!");
            Console.WriteLine("2. Nah, I don't care.");
            explicitContentSelect = Console.ReadLine();
        }
    }

    bool noProfanity = false;

    if (explicitContentSelect == "1")
        noProfanity = true;

    choices = new List<string>() { "1", "2", "3", "4", "5" };
    Console.WriteLine("How long is the show?  Pick between 1 - 5");
    Console.WriteLine("1. (10 songs)");
    Console.WriteLine("2. (15 songs)");
    Console.WriteLine("3. (20 songs)");
    Console.WriteLine("4. (25 songs)");
    Console.WriteLine("5. (30 songs)");
    string SetLength = Console.ReadLine();

    if (!choices.Contains(SetLength))
    {
        while (!choices.Contains(SetLength))
        {
            Console.WriteLine("How long is the show?  Pick between 1 - 5");
            Console.WriteLine("1. (10 songs)");
            Console.WriteLine("2. (15 songs)");
            Console.WriteLine("3. (20 songs)");
            Console.WriteLine("4. (25 songs)");
            Console.WriteLine("5. (30 songs)");
            SetLength = Console.ReadLine();
        }
    }

    int SetLengthValue = 0;
    switch (SetLength)
    {
        case "1":
            SetLengthValue = 10;
            TotalHours += 1;
            break;
        case "3":
            SetLengthValue = 15;
            TotalHours += 1.5;
            break;
        case "4":
            SetLengthValue = 20;
            TotalHours += 2;
            break;
        case "5":
            SetLengthValue = 25;
            TotalHours += 2.5;
            break;
        default:
            SetLengthValue = 30;
            TotalHours += 3;
            break;

    }

    SetRequest request = new SetRequest(SetLengthValue, Genres, noProfanity, decadeSelectToInt);

    Gig gig = new Gig(McFly, request);

    Console.WriteLine("Alright!  Time to rock the party!");
    foreach (Song song in gig.SongList)
    {

        Console.WriteLine("Next up is " + song.Title + " by " + song.Artists.GetCommaSeparatedString());
    }

    Console.WriteLine("Wanna do another gig?  Y/N");
    string newGig = Console.ReadLine();
    if (newGig.ToLower() != "y")
        KeepDoingGigs = false;

}

double TotalCost = McFly.HourlyRate * TotalHours;
Console.WriteLine("You made $" + TotalCost + " off these gigs!");