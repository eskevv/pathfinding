namespace Pathfinder;

class SquareGrid : IWeightedGraph<Location>
{
    public int Width { get; }
    public int Height { get; }
    public HashSet<Location> Walls { get; }
    public HashSet<Location> Forests { get; }

    public SquareGrid(int width, int height)
    {
        Width = width;
        Height = height;
        Walls = new HashSet<Location>();
        Forests = new HashSet<Location>();
    }

    public IEnumerable<Location> Neighbors(Location id)
    {
        foreach (var item in PossibleDirections())
        {
            Location next = new Location(id.X + item.X, id.Y + item.Y);
            if (InBounds(next) && Passable(next))
                yield return next;
        }
    }

    public double Cost(Location a, Location b)
    {
        return Forests.Contains(b) ? 5 : 1;
    }

    public void AddWall(int x, int y)
    {
        Walls.Add(new Location(x, y));
    }

    Location[] PossibleDirections()
    {
        return new Location[] 
        {
            new Location(1,0),
            new Location(0, -1),
            new Location(-1, 0),
            new Location(0, 1)
        };
    }

    bool InBounds(Location id)
    {
        return 0 <= id.X && id.X < Width && 0 <= id.Y && id.Y < Height;
    }

    bool Passable(Location id)
    {
        return !Walls.Contains(id);
    }
}