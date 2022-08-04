namespace Pathfinder;

interface IWeightedGraph<T>
{
    double Cost(T spot);

    IEnumerable<T> Neighbors(T id);
}

