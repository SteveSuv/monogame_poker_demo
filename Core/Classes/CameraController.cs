using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using MonoGame.Extended.ViewportAdapters;

class CameraController : Actor
{

    public static BoxingViewportAdapter viewportAdapter;
    public OrthographicCamera camera;
    // private OrthographicCamera originalCamera;

    public bool isShaking = false;

    public CameraController()
    {
        var virtualWidth = 1920 / 4;
        var virtualHeight = 1080 / 4;
        viewportAdapter = new BoxingViewportAdapter(MyGame.Window, MyGame.GraphicsDevice, virtualWidth, virtualHeight);
        camera = new OrthographicCamera(viewportAdapter) { MinimumZoom = 0.1f, MaximumZoom = 2f };
        // originalCamera = camera;
    }


    void MoveCamera(GameTime gameTime)
    {
        var dir = Vector2.Zero;
        var keyboard = KeyboardExtended.GetState();
        var speed = keyboard.IsShiftDown() ? 400 : 200;

        if (keyboard.IsKeyDown(Keys.Up))
        {
            dir -= Vector2.UnitY;
        }
        if (keyboard.IsKeyDown(Keys.Left))
        {
            dir -= Vector2.UnitX;
        }
        if (keyboard.IsKeyDown(Keys.Down))
        {
            dir += Vector2.UnitY;
        }
        if (keyboard.IsKeyDown(Keys.Right))
        {
            dir += Vector2.UnitX;
        }

        camera.Move(dir * speed * gameTime.GetElapsedSeconds());

    }
    void ZoomCamera(GameTime gameTime)
    {
        var mouse = MouseExtended.GetState();
        var keyboard = KeyboardExtended.GetState();
        var speed = keyboard.IsShiftDown() ? 5 : 1;

        if (mouse.DeltaScrollWheelValue > 0)
        {
            camera.ZoomOut(speed * gameTime.GetElapsedSeconds());
        }

        if (mouse.DeltaScrollWheelValue < 0)
        {
            camera.ZoomIn(speed * gameTime.GetElapsedSeconds());

        }
    }
    void RotateCamera(GameTime gameTime)
    {
        var keyboard = KeyboardExtended.GetState();
        var speed = keyboard.IsShiftDown() ? 5 : 1;

        if (keyboard.IsKeyDown(Keys.Q))
        {
            camera.Rotate(speed * gameTime.GetElapsedSeconds());
        }
        if (keyboard.IsKeyDown(Keys.E))
        {
            camera.Rotate(-speed * gameTime.GetElapsedSeconds());
        }
    }
    void PitchCamera(GameTime gameTime)
    {
        var keyboard = KeyboardExtended.GetState();
        var isShiftDown = keyboard.IsShiftDown();
        var speed = isShiftDown ? 5 : 1;

        if (keyboard.IsKeyDown(Keys.R))
        {
            camera.PitchUp(speed * gameTime.GetElapsedSeconds());
        }
        if (keyboard.IsKeyDown(Keys.T))
        {
            camera.PitchDown(speed * gameTime.GetElapsedSeconds());
        }
    }

    void shakeCamera(GameTime gameTime)
    {
        if (isShaking)
        {
            Vector2 vec2;
            Random.Shared.NextUnitVector(out vec2);
            camera.Move(vec2);
        }

    }

    public static void resetCamera()
    {

    }

    public override void Update(GameTime gameTime)
    {
        MoveCamera(gameTime);
        ZoomCamera(gameTime);
        RotateCamera(gameTime);
        PitchCamera(gameTime);
        shakeCamera(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        if (MyGame.IsDebug)
        {
            MyGame.SpriteBatch.DrawRectangle(camera.BoundingRectangle, MyGame.DebugColor);
            MyGame.SpriteBatch.DrawPoint(camera.Center, MyGame.DebugColor, 4);
        }
    }

    public override Vector2 GetSize()
    {
        return camera.BoundingRectangle.Size;
    }
}