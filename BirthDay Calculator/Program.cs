using System.Dynamic;
using System.Globalization;

namespace HelloWorld;

internal class Program
{
    private static void Main(string[] args)
    {
        // Introduce our Savvy robot
        IntroduceSavvy();

        // Ask the user for their date of birth
        var userDateOfBirth = AskForDateOfBirth();

        //Announcee information about date

        AnnounceDateOfBirthInformation(userDateOfBirth);
    }

    /// <summary>
    /// Introduces the Savvy robot to our user
    /// </summary>
    public static void IntroduceSavvy()
    {
        //Output message to user
        Console.WriteLine("Hi, I'm Savvy." +
            "I'm really good at guessing the " +
            "day of the week you were born on.");
    }

    /// <summary>
    /// Asks the user for their date of birth, until it is given in a correct format.
    /// </summary>
    /// <remarks>This call will not return until the user entered or the application
    /// is terminated.
    /// </remarks>
    /// <returns>The date of birth the user entered</returns>
    public static DateTimeOffset AskForDateOfBirth()
    {
        // Create checked date with intial out of range value.
        var dateofBirth = DateTimeOffset.MaxValue;

        // Until the checked date is Greater than today...
        while (dateofBirth > DateTimeOffset.UtcNow)
        {
            // Tell the your to enter their date of birth
            Console.WriteLine($"Can I Start by asking you " +
          $"what your date of birth is? {CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}");

            //Read their Response
            var userText = Console.ReadLine();

            // If the user entered a valid date...
            if (DateTimeOffset.TryParse(userText, out var parseDate))
                // Set the checked date to users given date
                dateofBirth = parseDate;
            // Otherwise...
            else
                //Inform user they entered invalid date
                Console.WriteLine("You did not enter a valid date");
        }
        //Return the result
        return dateofBirth;
    }

    /// <summary>
    /// Display Information to the user about their date of birth
    /// </summary>
    /// <param name="date">The date of birth to display information about</param>
    public static void AnnounceDateOfBirthInformation(DateTimeOffset date)
    {
        //Store current Time
        var now = DateTimeOffset.UtcNow;

        //Display date of birth
        Console.WriteLine($"So you were born on {date:MMMM dd yyyy}");

        //Display day of the week they were born
        Console.WriteLine($"That was a {date.DayOfWeek}");

        //Indicate if we have already passed the users date of birth
        //for the current year
        var hasPassedBirthDay = now.DayOfYear > date.DayOfYear;

        //Get a date representing the users next birthday
        var nextBirthday = new DateTimeOffset(now.Year + (hasPassedBirthDay ? 1 : 0), date.Month, date.Day, 0, 0, 0, TimeSpan.Zero);

        //Display how many days until the next birthday
        Console.WriteLine($"It is {(nextBirthday - now).TotalDays:0} days until your next birthday");

        //Get the users age
        var userAge = now.Year - date.Year - (hasPassedBirthDay ? 0 : 1);

        //Display user age
        Console.WriteLine($"You are {userAge} years old");

        //Display users age in dog years
        Console.WriteLine($"You are {userAge * 7} dog years old");

        //Get users time alive
        var timeAlive = now - date;

        //Display days old
        Console.WriteLine($"You have been alive for {timeAlive.TotalDays:n0} days");

        //Display hours old
        Console.WriteLine($"You have been alive for {timeAlive.TotalHours:n0} hours");

        //Display seconds old
        Console.WriteLine($"You have been alive for {timeAlive.TotalSeconds:n0} seconds");
    }
}