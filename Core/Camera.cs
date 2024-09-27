using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using MonoGame.Extended.ViewportAdapters;

class Camera
{
    private OrthographicCamera _camera;


    public Camera(int virtualWidth = 800, int virtualHeight = 480)
    {
        var viewportAdapter = new BoxingViewportAdapter(MyGame.instance.Window, MyGame.instance.GraphicsDevice, virtualWidth, virtualHeight);
        _camera = new OrthographicCamera(viewportAdapter);
    }

    public Matrix GetViewMatrix()
    {
        return _camera.GetViewMatrix();
    }


    public void Update(GameTime gameTime)
    {
        MoveCamera(gameTime);
        ZoomCamera(gameTime);
        RotateCamera(gameTime);
        PitchCamera(gameTime);
    }



    void MoveCamera(GameTime gameTime)
    {
        var dir = Vector2.Zero;
        var keyboard = KeyboardExtended.GetState();
        var speed = keyboard.IsShiftDown() ? 500 : 200;

        if (keyboard.IsKeyDown(Keys.W))
        {
            dir -= Vector2.UnitY;
        }
        if (keyboard.IsKeyDown(Keys.A))
        {
            dir -= Vector2.UnitX;
        }
        if (keyboard.IsKeyDown(Keys.S))
        {
            dir += Vector2.UnitY;
        }
        if (keyboard.IsKeyDown(Keys.D))
        {
            dir += Vector2.UnitX;
        }


        _camera.Move(dir * speed * gameTime.GetElapsedSeconds());

    }

    void ZoomCamera(GameTime gameTime)
    {
        var keyboard = KeyboardExtended.GetState();
        var speed = keyboard.IsShiftDown() ? 5 : 1;

        if (keyboard.IsKeyDown(Keys.Z))
        {
            _camera.ZoomIn(speed * gameTime.GetElapsedSeconds());
        }
        if (keyboard.IsKeyDown(Keys.X))
        {
            _camera.ZoomOut(speed * gameTime.GetElapsedSeconds());
        }
    }


    void RotateCamera(GameTime gameTime)
    {
        var keyboard = KeyboardExtended.GetState();
        var speed = keyboard.IsShiftDown() ? 5 : 1;

        if (keyboard.IsKeyDown(Keys.Q))
        {
            _camera.Rotate(speed * gameTime.GetElapsedSeconds());
        }
        if (keyboard.IsKeyDown(Keys.E))
        {
            _camera.Rotate(-speed * gameTime.GetElapsedSeconds());
        }
    }


    void PitchCamera(GameTime gameTime)
    {
        var keyboard = KeyboardExtended.GetState();
        var isShiftDown = keyboard.IsShiftDown();
        var speed = isShiftDown ? 5 : 1;

        if (keyboard.IsKeyDown(Keys.R))
        {
            _camera.PitchUp(speed * gameTime.GetElapsedSeconds());
        }
        if (keyboard.IsKeyDown(Keys.T))
        {
            _camera.PitchDown(speed * gameTime.GetElapsedSeconds());
        }
    }


}