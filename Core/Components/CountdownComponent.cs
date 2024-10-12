using Microsoft.Xna.Framework;
using MonoGame.Extended.Timers;

class CountdownComponent : Component
{
    public double intervalSeconds = 1;
    private CountdownTimer countdownTimer;
    public EventHandler Completed = (object sender, EventArgs e) => { };

    public override void Initialize()
    {
        countdownTimer = new(intervalSeconds);
        countdownTimer.Completed += Completed;
    }

    public override void Update(GameTime gameTime)
    {
        countdownTimer.Update(gameTime);
    }
}

