namespace Pathfinder;

class BreadthFirstSearch
{
    public int CellSize { get; }
    public SquareGrid Grid { get; }
    public HashSet<Location> Empty { get; }
    public Queue<Location> Frontier { get; }
    public Dictionary<Location, Location> CameFrom { get; }

    public BreadthFirstSearch(SquareGrid grid, int cellSize, Location start)
    {
        Grid = grid;
        CellSize = cellSize;
        Frontier = new Queue<Location>();
        CameFrom = new Dictionary<Location, Location>();
        Empty = new HashSet<Location>();

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                Empty.Add(new Location(x, y));
            }
        }

        Frontier.Enqueue(start);
        CameFrom[start] = start;
    }

    public void Search()
    {
        int loops = Frontier.Count;
        for (int x = 0; x < loops; x++)
        {
            GetFrontiers();
        }
    }

    void GetFrontiers()
    {
        if (Frontier.Count <= 0) return;

        Location current = Frontier.Dequeue();

        foreach (var item in Grid.Neighbors(current))
        {
            if (CameFrom.ContainsKey(item))
                continue;

            Frontier.Enqueue(item);
            CameFrom[item] = current;
            Empty.Remove(item);
        }
    }
}