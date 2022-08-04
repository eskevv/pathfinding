namespace Pathfinder;

interface IWeightedGraph<T>
{
    double Cost(T a, T b);

    IEnumerable<T> Neighbors(T id);
}

