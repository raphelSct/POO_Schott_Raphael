using Models.SpaceShips;

namespace Models;

public class Player : IPlayer
{
    private string FirstName { get; }
    private string LastName { get; }

    public string Name => $"{FirstName} {LastName}";
    public string Alias { get; }
    public Spaceship BattleShip { get; set; }

    public Player(string firstName, string lastName, string alias)
    {
        FirstName = FormatName(firstName);
        LastName = FormatName(lastName);
        Alias = alias;
        BattleShip = new Spaceship("ViperMKII", 10, 15);
    }

    private static string FormatName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return name;
        }
        return name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
    }

    public override string ToString()
    {
        return $"{Alias} ({Name})";
    }

    public override bool Equals(object? obj)
    {
        return obj is Player other && Alias == other.Alias;
    }

    public override int GetHashCode()
    {
        return Alias.GetHashCode();
    }
}
