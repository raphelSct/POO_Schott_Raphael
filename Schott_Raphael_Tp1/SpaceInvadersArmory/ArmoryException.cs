namespace SpaceInvadersArmory;

public class ArmoryException : Exception
{
    public ArmoryException() : base("Impossible d'ajouter une arme inexistante dans l'armurerie.")
    {

    }
}