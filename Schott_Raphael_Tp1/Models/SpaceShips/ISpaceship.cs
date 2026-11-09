using System.Collections.Generic;
using SpaceInvadersArmory;

namespace Models
{
    public interface ISpaceship
    {
        string Name { get; }
        double MaxStructure { get; }
        double MaxShield { get; }
        double CurrentStructure { get; }
        double CurrentShield { get; }
        bool IsDestroyed { get; }
        int MaxWeapons { get; }
        IReadOnlyList<Weapon> Weapons { get; }
        double AverageDamages { get; }
        void AddWeapon(Weapon weapon);
        void RemoveWeapon(Weapon weapon);
        void ClearWeapons();
        void ViewShip();
        void ViewWeapons();
    }
}