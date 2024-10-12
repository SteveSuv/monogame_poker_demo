using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tweening;

class BootScreen(MyGame game) : GameScreen(game)
{


    private readonly Node world = new()
    {
        localPosition = MyGame.ScreenCenter,
    };

    public override void Initialize()
    {
        base.Initialize();

        world.countdownTimers.Add(new(2), (_, _) =>
        {
            MyGame.LoadScreen(new StartScreen(game));
        });

        var labelNode = new LabelNode() { text = "Tommy Games Production", fontSize = 60, color = Color.Yellow, localPosition = new(0, 100) };

        labelNode.tweenerActions.Add(t => t.TweenTo(target: labelNode, expression: e => e.localPosition, toValue: new(0, 0), duration: 1));

        world.AddChild(labelNode);

        world.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        world.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.GraphicsDevice.Clear(Color.Black);
        MyGame.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        world.Draw();
        MyGame.SpriteBatch.End();
    }
}