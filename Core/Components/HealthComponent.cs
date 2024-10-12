using Microsoft.Xna.Framework;

class HealthComponent : Component
{
    public int health;
    public int maxHealth = 100;

    public EventHandler<int> OnDamage;

    public HealthComponent()
    {
        health = maxHealth;
    }

    public override void Update(GameTime gameTime)
    {

    }
}

