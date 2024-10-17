
using Microsoft.Xna.Framework;
using MonoGame.Extended;

class Node
{
    public string ID;
    public List<Node> Children => NodeManager.children;
    public NodeManager NodeManager;
    public ComponentManager ComponentManager;
    public Node parent;
    public string tag;
    public Vector2 localPosition = Vector2.Zero;
    public Vector2 WorldPosition
    {
        get
        {
            if (parent == null)
            {
                return localPosition;
            }
            return parent.WorldPosition + localPosition;
        }
        set
        {
            if (parent == null)
            {
                localPosition = value;
            }
            else
            {
                localPosition = value - parent.WorldPosition;
            }
        }
    }
    public float rotation = 0;
    public Vector2 scale = Vector2.One;
    public Vector2 origin = Origin.Center;
    public Color color = Color.White;
    private float _layerDepth = 0;
    public float LayerDepth
    {
        get
        {
            if (_layerDepth != 0) return _layerDepth;
            if (parent == null) return _layerDepth;
            return parent.LayerDepth;
        }
        set
        {
            _layerDepth = value;
        }
    }
    public Vector2 Size => GetSize();
    public Vector2 OriginOffset => origin * Size;
    public RectangleF Rectangle => new(WorldPosition - OriginOffset, Size);

    public Node()
    {
        ID = Guid.NewGuid().ToString();
        NodeManager = new(this);
        ComponentManager = new(this);
    }

    public virtual void Initialize()
    {
        NodeManager.Initialize();
        ComponentManager.Initialize();
    }

    public virtual void Update(GameTime gameTime)
    {
        NodeManager.Update(gameTime);
        ComponentManager.Update(gameTime);
    }

    public virtual void Draw()
    {
        NodeManager.Draw();
    }

    public virtual Vector2 GetSize()
    {
        return new();
    }

    public void RemoveFromParent()
    {
        parent?.NodeManager.RemoveChild(this);
    }
}