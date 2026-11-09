namespace SpaceInvadersArmory;

public class Weapon : IWeapon
{
    public string Name { get; }
    public EWeaponType Type { get; }
    public double MinDamage { get; }
    public double MaxDamage { get; }
    public double AverageDamage => (MinDamage + MaxDamage) / 2;
    public int ReloadTime { get; }
    public int TimeBeforeReload { get; private set; }
    public bool IsReady => TimeBeforeReload == 0;

    public Weapon(string name, double minDamage, double maxDamage, EWeaponType type, int reloadTime)
    {
        Name = name;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        Type = type;
        // Une arme explosive met deux fois plus de temps à recharger
        ReloadTime = type == EWeaponType.Explosive ? reloadTime * 2 : reloadTime;
        TimeBeforeReload = ReloadTime;
    }

    public void DecrementReload()
    {
        if (TimeBeforeReload > 0)
        {
            TimeBeforeReload--;
        }
    }

    public double Shoot()
    {
        if (!IsReady)
        {
            return 0;
        }
        TimeBeforeReload = ReloadTime;

        switch (Type)
        {
            case EWeaponType.Direct:
                // 1 chance sur 10 de rater : Next(10) renvoie un entier de 0 à 9
                return Random.Shared.Next(10) == 0 ? 0 : RandomDamage();

            case EWeaponType.Explosive:
                // 1 chance sur 4 de rater, dégâts doublés sinon
                return Random.Shared.Next(4) == 0 ? 0 : RandomDamage() * 2;

            case EWeaponType.Guided:
                // Touche toujours, mais avec les dégâts minimums
                return MinDamage;

            default:
                return RandomDamage();
        }
    }

    public override string ToString()
    {
        return $"{Name} ({Type}) : {MinDamage} à {MaxDamage} dégâts, recharge {ReloadTime} tour(s)";
    }

    private double RandomDamage()
    {
        return MinDamage + Random.Shared.NextDouble() * (MaxDamage - MinDamage);
    }
}
