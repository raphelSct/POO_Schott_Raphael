using Models;
using Models.SpaceShips;
using SpaceInvadersArmory;

namespace Schott_Raphael_Tp1;

public class SpaceInvaders
{
    private readonly List<Player> _players = new List<Player>();
    private readonly Armory _armory;

    public SpaceInvaders()
    {
        _armory = Armory.Instance;
        Init();
    }

    private void Init()
    {
        // Casse volontairement mélangée pour vérifier le formatage de Player
        _players.Add(new Player("tHOMAS", "anderson", "Neo"));
        _players.Add(new Player("EMMET", "brown", "Doc"));
        _players.Add(new Player("ellen", "RiPlEy", "Ripley"));
    }

    static void Main(string[] args)
    {
        SpaceInvaders spaceInvaders = new SpaceInvaders();
        foreach (Player player in spaceInvaders._players)
        {
            Console.WriteLine(player.ToString());
        }

        // Affichage de l'armurerie
        Console.WriteLine();
        spaceInvaders._armory.ViewArmory();

        // Équipement du vaisseau du premier joueur avec deux armes de l'armurerie
        Spaceship firstShip = spaceInvaders._players[0].BattleShip;
        firstShip.AddWeapon(spaceInvaders._armory.Weapons[0]);
        firstShip.AddWeapon(spaceInvaders._armory.Weapons[1]);
        Console.WriteLine();
        firstShip.ViewShip();

        // La 3e arme passe, la 4e dépasse MaxWeapons et lève une exception
        Console.WriteLine();
        try
        {
            firstShip.AddWeapon(spaceInvaders._armory.Weapons[2]);
            firstShip.AddWeapon(spaceInvaders._armory.Weapons[0]);
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine($"Erreur : {exception.Message}");
        }

        // Bonus 11.a : une arme absente de l'armurerie est refusée par le vaisseau
        Spaceship secondShip = spaceInvaders._players[1].BattleShip;
        try
        {
            secondShip.AddWeapon(new Weapon("Sabre laser", 1, 2, EWeaponType.Direct, 1));
        }
        catch (ArmoryException exception)
        {
            Console.WriteLine($"Erreur : {exception.Message}");
        }
    }
}
