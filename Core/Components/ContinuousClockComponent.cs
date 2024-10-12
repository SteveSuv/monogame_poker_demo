
using Microsoft.Xna.Framework;
using MonoGame.Extended.Timers;

class ContinuousClockComponent : Component
{
    public double intervalSeconds = 1;
    private ContinuousClock continuousClock;
    public EventHandler Tick = (object sender, EventArgs e) => { };

    public override void Initialize()
    {
        continuousClock = new(intervalSeconds);
        continuousClock.Tick += Tick;
    }

    public override void Update(GameTime gameTime)
    {
        continuousClock.Update(gameTime);
    }
}

