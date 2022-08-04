namespace Pathfinder;

class BreadthFirstSearch
{
    public SquareGrid Grid { get; }
    public HashSet<Location> Unexplored { get; }
    public Queue<Location> Frontier { get; }
    public Dictionary<Location, Location> CameFrom { get; }

    public BreadthFirstSearch(SquareGrid grid, Location start)
    {
        Grid = grid;
        Frontier = new Queue<Location>();
        CameFrom = new Dictionary<Location, Location>();
        Unexplored = new HashSet<Location>();

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                var location = new Location(x, y);
                if (location.Equals(start))
                    continue;

                Unexplored.Add(location);
            }
        }

        Frontier.Enqueue(start);
        CameFrom[start] = start;
    }

    public void SearchGrid()
    {
        while (Frontier.Count > 0)
        {
            Search();
        }
    }

    public void SearchFrontiers()
    {
        int count = Frontier.Count;

        for (int x = 0; x < count; x++)
        {
            Search();
        }
    }

    void Search()
    {
        Location current = Frontier.Dequeue();

        foreach (var item in Grid.Neighbors(current))
        {
            if (CameFrom.ContainsKey(item))
                continue;

            Frontier.Enqueue(item);
            CameFrom[item] = current;
            Unexplored.Remove(item);
        }
    }
}