using Microsoft.Xna.Framework;

class TransformComponent : Component
{
    public Vector2 localPosition = Vector2.Zero;
    public Vector2 worldPosition
    {
        get
        {
            if (belong.parent == null)
            {
                return localPosition;
            }
            return belong.parent.transform.worldPosition + localPosition;
        }
    }
    public float rotation = 0;
    public Vector2 scale = Vector2.One;
    public Vector2 origin = Origin.Center;
    public Color color = Color.White;
    public float layerDepth = 0;
}
