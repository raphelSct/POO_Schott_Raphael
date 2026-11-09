

using Models.SpaceShips;

namespace Models
{
    public interface IPlayer
    {
        string Name { get; }
        string Alias { get; }
        Spaceship BattleShip { get; set; }
    }
}