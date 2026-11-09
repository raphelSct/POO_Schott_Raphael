using SpaceInvadersArmory;

namespace Models.SpaceShips;

public class Spaceship : ISpaceship
{
    private readonly List<Weapon> _weapons = new List<Weapon>();

    public string Name { get; }
    public double MaxStructure { get; }
    public double MaxShield { get; }
    public double CurrentStructure { get; private set; }
    public double CurrentShield { get; private set; }
    public bool IsDestroyed => CurrentStructure <= 0;
    public int MaxWeapons => 3;
    public IReadOnlyList<Weapon> Weapons => _weapons;
    public double AverageDamages => _weapons.Sum(weapon => weapon.AverageDamage);

    public Spaceship(string name, double maxStructure, double maxShield)
    {
        Name = name;
        MaxStructure = maxStructure;
        MaxShield = maxShield;
        CurrentStructure = MaxStructure;
        CurrentShield = MaxShield;
    }

    public void AddWeapon(Weapon weapon)
    {
        // ISpaceship.AddWeapon(Weapon) ne peut pas recevoir de paramètre supplémentaire.
        // Il n’y a qu’une armurerie dans le jeu, donc une instance statique unique a du sens.
        // Le constructeur privé garantit qu’aucune autre armurerie ne peut être créée.
        // Contrairement à une propriété publique, l’armurerie ne peut être ni null ni remplacée.

        if (!Armory.Instance.Contains(weapon))
        {
            throw new ArmoryException();
        }
        if (_weapons.Count >= MaxWeapons)
        {
            throw new InvalidOperationException($"Le vaisseau {Name} ne peut pas porter plus de {MaxWeapons} armes.");
        }
        _weapons.Add(weapon);
    }

    // Question 6.6 : List.Remove cherche l'élément à retirer en appelant Equals.
    // Weapon ne redéfinit pas Equals, c'est donc celui d'Object qui compare les références :
    // seule l'instance exacte ajoutée au vaisseau est retrouvée. Une autre instance de Weapon
    // portant le même nom n'est pas considérée comme égale : Remove renvoie false et rien n'est retiré.
    // Pour retirer par nom, il faudrait redéfinir Equals (et GetHashCode) dans Weapon, comme pour Player.

    public void RemoveWeapon(Weapon weapon)
    {
        _weapons.Remove(weapon);
    }

    public void ClearWeapons()
    {
        _weapons.Clear();
    }

    public void ViewWeapons()
    {
        if (_weapons.Count == 0)
        {
            Console.WriteLine("  Aucune arme");
            return;
        }

        foreach (Weapon weapon in _weapons)
        {
            Console.WriteLine($"  - {weapon}");
        }
    }

    public void ViewShip()
    {
        Console.WriteLine($"=== {Name} ===");
        Console.WriteLine($"Structure : {CurrentStructure} / {MaxStructure}");
        Console.WriteLine($"Bouclier  : {CurrentShield} / {MaxShield}");
        Console.WriteLine($"Armes ({_weapons.Count}/{MaxWeapons}) :");
        ViewWeapons();
        Console.WriteLine($"Dégâts moyens : {AverageDamages}");
    }
}
