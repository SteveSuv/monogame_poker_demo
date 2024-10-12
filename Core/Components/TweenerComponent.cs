using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Tweening;

class TweenerComponent : Component
{
    public Action<Tweener, Node> tweenerAction;
    private Tweener tweener;

    public override void Initialize()
    {
        tweener = new Tweener();
        tweenerAction(tweener, belong);
    }

    public override void Update(GameTime gameTime)
    {
        tweener.Update(gameTime.GetElapsedSeconds());
    }
}

