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
        _algorithm = new BreadthFirstSearch(new SquareGrid(96, 54), new Location(12, 13));
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

        if (_time++ >= 9)
        {
            _time = 0;
            _algorithm.SearchFrontiers();
        }

        if (Input.IsHeld(MouseButton.LeftButton))
            CreateWall(Input.MousePosition.X, Input.MousePosition.Y);


        base.Update(gameTime);
    }

    void CreateWall(float xPos, float yPos)
    {
        int x = (int)(xPos / 20);
        int y = (int)(yPos / 20);

        var location = new Location(x, y);
        if (_algorithm.CameFrom.ContainsKey(location))
            _algorithm.CameFrom.Remove(location);

        _algorithm.Grid.AddWall(x, y);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(49, 49, 49));

        _shapebatch.Begin();

        int cellSize = 20;
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
            _shapebatch.DrawFillRect(xx, yy, cellSize, cellSize, Color.GhostWhite);
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
            _shapebatch.DrawFillRect(xx, yy, cellSize, cellSize, new Color(60, 60, 180, 90));
        }

        _shapebatch.End();

        base.Draw(gameTime);
    }
}
