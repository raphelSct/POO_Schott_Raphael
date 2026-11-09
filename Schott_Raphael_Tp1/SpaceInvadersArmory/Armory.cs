namespace SpaceInvadersArmory;

public class Armory
{
    private readonly List<Weapon> _weapons = new List<Weapon>();

    public IReadOnlyList<Weapon> Weapons => _weapons;

    public static Armory Instance { get; } = new Armory();

    // Constructeur privé : la seule armurerie du jeu est Armory.Instance
    private Armory()
    {
        Init();
    }

    private void Init()
    {
        // Liste minimum du sujet (temps de rechargement de base, doublé pour les explosives)
        _weapons.Add(new Weapon("Laser", 2, 3, EWeaponType.Direct, 2));
        _weapons.Add(new Weapon("Hammer", 1, 8, EWeaponType.Explosive, 2));
        _weapons.Add(new Weapon("Torpille", 3, 3, EWeaponType.Guided, 2));
        _weapons.Add(new Weapon("Mitrailleuse", 6, 8, EWeaponType.Direct, 1));
        _weapons.Add(new Weapon("EMG", 1, 7, EWeaponType.Explosive, 2));
        _weapons.Add(new Weapon("Missile", 4, 100, EWeaponType.Guided, 4));

        // Armes supplémentaires
        _weapons.Add(new Weapon("Canon Laser", 5, 20, EWeaponType.Direct, 2));
        _weapons.Add(new Weapon("Bombe à fragmentation", 20, 50, EWeaponType.Explosive, 3));
        _weapons.Add(new Weapon("Guidage thermique", 1, 10, EWeaponType.Guided, 2));
    }

    public void ViewArmory()
    {
        Console.WriteLine($"=== Armurerie ({_weapons.Count} armes) ===");
        foreach (Weapon weapon in _weapons)
        {
            Console.WriteLine($"  - {weapon}");
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
    }

    public void RemoveWeapon(Weapon weapon)
    {
        _weapons.Remove(weapon);
    }

    public bool Contains(Weapon weapon)
    {
        return _weapons.Any(armoryWeapon => armoryWeapon.Name == weapon.Name);
    }
}
