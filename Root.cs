using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OrionLibrary;

namespace Pathfinder;

public class Root : Game
{
    private const int ScreenWidth = 1920;
    private const int ScreenHeight = 1080;
    private const int TargetFps = 120;
    private const bool IsFullScreen = false;
    private const bool IsVsync = false;
    private const bool IsBorderless = true;

    private GraphicsDeviceManager _graphics;
    private BreadthFirstSearch _algorithm;

    private SpriteBatch _spriteBatch = null!;
    private ShapeBatch _shapebatch = null!;
    private int _time;

    public Root()
    {
        _graphics = new GraphicsDeviceManager(this);
        _algorithm = new BreadthFirstSearch(new SquareGrid(48, 27), new Location(12, 13));
        _algorithm.Grid.AddWall(29, 21);
        _algorithm.Grid.AddWall(30, 21);
        _algorithm.Grid.AddWall(31, 21);
        _algorithm.Grid.AddWall(32, 21);
        _algorithm.Grid.AddWall(33, 21);
        _algorithm.Grid.AddWall(34, 21);
        _algorithm.Grid.AddWall(35, 21);
        _algorithm.Grid.AddWall(36, 21);
        _algorithm.Grid.AddWall(37, 21);
        _algorithm.Grid.AddWall(38, 21);
        _algorithm.Grid.AddWall(39, 21);
        _algorithm.Grid.AddWall(40, 21);
        _algorithm.Grid.AddWall(41, 21);
        _algorithm.Grid.AddWall(42, 21);
        _algorithm.Grid.AddWall(43, 21);
        _algorithm.Grid.AddWall(43, 22);
        _algorithm.Grid.AddWall(43, 23);
        _algorithm.Grid.AddWall(43, 24);
        _algorithm.Grid.AddWall(43, 25);
        _algorithm.Grid.AddWall(43, 26);
        _algorithm.Grid.AddWall(43, 27);
        _algorithm.Grid.AddWall(35, 7);
        _algorithm.Grid.AddWall(35, 6);
        _algorithm.Grid.AddWall(35, 5);
        _algorithm.Grid.AddWall(35, 4);
        _algorithm.Grid.AddWall(34, 4);
        _algorithm.Grid.AddWall(33, 4);
        _algorithm.Grid.AddWall(32, 4);
        _algorithm.Grid.AddWall(31, 4);
        _algorithm.Grid.AddWall(30, 4);
        _algorithm.Grid.AddWall(30, 5);
        _algorithm.Grid.AddWall(30, 6);
        _algorithm.Grid.AddWall(30, 7);
        _algorithm.Grid.AddWall(30, 8);
        _algorithm.Grid.AddWall(30, 9);
        _algorithm.Grid.AddWall(30, 10);
        _algorithm.Grid.AddWall(30, 11);
        _algorithm.Grid.AddWall(30, 12);
        _algorithm.Grid.AddWall(30, 13);
        _algorithm.Grid.AddWall(31, 13);
        _algorithm.Grid.AddWall(32, 13);
        _algorithm.Grid.AddWall(33, 13);
        _algorithm.Grid.AddWall(34, 13);
        _algorithm.Grid.AddWall(35, 13);
        _algorithm.Grid.AddWall(36, 13);
        _algorithm.Grid.AddWall(37, 13);
        _algorithm.Grid.AddWall(38, 13);
        _algorithm.Grid.AddWall(39, 13);
        _algorithm.Grid.AddWall(24, 0);
        _algorithm.Grid.AddWall(24, 1);
        _algorithm.Grid.AddWall(24, 2);
        _algorithm.Grid.AddWall(24, 3);
        _algorithm.Grid.AddWall(24, 4);
        _algorithm.Grid.AddWall(24, 5);
        _algorithm.Grid.AddWall(24, 6);
        _algorithm.Grid.AddWall(24, 7);
        _algorithm.Grid.AddWall(24, 8);
        _algorithm.Grid.AddWall(24, 9);
        _algorithm.Grid.AddWall(24, 10);
        _algorithm.Grid.AddWall(24, 11);
        _algorithm.Grid.AddWall(24, 12);
        _algorithm.Grid.AddWall(24, 13);
        _algorithm.Grid.AddWall(24, 14);
        _algorithm.Grid.AddWall(24, 15);
        _algorithm.Grid.AddWall(24, 16);
        _algorithm.Grid.AddWall(24, 17);
        _algorithm.Grid.AddWall(24, 18);
        _algorithm.Grid.AddWall(24, 19);
        _algorithm.Grid.AddWall(24, 20);
        _algorithm.Grid.AddWall(24, 21);
        _algorithm.Grid.AddWall(24, 22);
        _algorithm.Grid.AddWall(24, 23);
        _algorithm.Grid.AddWall(24, 24);
        _algorithm.Grid.AddWall(24, 25);
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        _graphics.IsFullScreen = IsFullScreen;
        _graphics.SynchronizeWithVerticalRetrace = IsVsync;
        _graphics.ApplyChanges();

        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _shapebatch = new ShapeBatch(GraphicsDevice);

        TargetElapsedTime = TimeSpan.FromSeconds(1d / TargetFps);
        Window.IsBorderless = IsBorderless;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        base.Initialize();
    }

    protected override void LoadContent()
    {

    }

    protected override void Update(GameTime gameTime)
    {
        Input.Update();

        if (Input.Pressed(Keys.Escape))
            Exit();

        if (_time++ >= 18)
        {
            _time = 0;
            _algorithm.SearchFrontiers();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(49, 49, 49));
        _shapebatch.Begin();

        int cellSize = 40;
        foreach (var item in _algorithm.Unexplored)
        {
            int xx = item.X * cellSize;
            int yy = item.Y * cellSize;
            _shapebatch.DrawRect(xx, yy, cellSize, cellSize, Color.WhiteSmoke);
        }

        foreach (var item in _algorithm.CameFrom.Keys)
        {
            if (_algorithm.Frontier.Contains(item))
                continue;
            int xx = item.X * cellSize;
            int yy = item.Y * cellSize;
            _shapebatch.DrawFillRect(xx, yy, cellSize, cellSize, Color.Gray);
        }
        foreach (var item in _algorithm.CameFrom.Keys)
        {
            if (_algorithm.Frontier.Contains(item))
                continue;
            int xx = item.X * cellSize;
            int yy = item.Y * cellSize;
            _shapebatch.DrawRect(xx, yy, cellSize, cellSize, Color.WhiteSmoke);
        }

        foreach (var item in _algorithm.Frontier)
        {
            int xx = item.X * cellSize;
            int yy = item.Y * cellSize;
            _shapebatch.DrawRect(xx, yy, cellSize, cellSize, Color.Red, 3);
        }

        foreach (var item in _algorithm.Grid.Walls)
        {
            int xx = item.X * cellSize;
            int yy = item.Y * cellSize;
            _shapebatch.DrawFillRect(xx, yy, cellSize, cellSize, Color.DimGray);
        }

        _shapebatch.End();

        base.Draw(gameTime);
    }
}
