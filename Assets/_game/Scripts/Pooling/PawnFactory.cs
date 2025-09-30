using Zenject;

public class PawnFactory : IPawnFactory
{
    private readonly DiContainer _container;

    public PawnFactory(DiContainer container)
    {
        _container = container;
    }

    public Pawn CreatePawn(Pawn prefab)
    {
        return _container.InstantiatePrefabForComponent<Pawn>(prefab);
    }
}

public interface IPawnFactory
{
    Pawn CreatePawn(Pawn prefab);
}