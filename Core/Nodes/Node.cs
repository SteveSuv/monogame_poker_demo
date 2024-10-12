
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using MonoGame.Extended.Timers;
using MonoGame.Extended.Tweening;

class Node
{
    public List<Node> children = [];
    // public List<Component> components = [];
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
    public float layerDepth
    {
        get
        {
            if (parent == null) return 0;
            return parent.layerDepth;
        }
        set { }
    }
    public Vector2 Size => GetSize();
    public Vector2 OriginOffset => origin * Size;
    public RectangleF Rectangle => new(WorldPosition - OriginOffset, Size);
    private bool isHover = false;
    public List<Action<Tweener>> tweenerActions = [];
    private readonly List<Tweener> tweeners = [];
    public Dictionary<CountdownTimer, EventHandler> countdownTimers = [];
    public Dictionary<ContinuousClock, EventHandler> continuousClocks = [];

    public EventHandler<Vector2> OnMouseEnter = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnMouseDown = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnMouseUp = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnClick = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnMouseMove = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnMouseLeave = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnOutSideClick = (object sender, Vector2 mousePos) => { };

    // public Node AddComponent(Component component)
    // {
    //     component.belong = this;
    //     components.Add(component);
    //     return this;
    // }

    public Node AddChild(Node node)
    {
        node.parent = this;
        children.Add(node);
        return this;
    }

    // public T GetComponent<T>() where T : Component
    // {
    //     return components.Find(x => x is T) as T;
    // }


    public T GetChild<T>() where T : Node
    {
        return children.Find(x => x is T) as T;
    }

    public T GetChildByTag<T>(string tag) where T : Node
    {
        return children.Find(x => x.tag == tag) as T;
    }

    public void RemoveChildByTag(string tag)
    {
        children = children.FindAll(x => x.tag != tag);
    }

    public void RemoveChild(Node node)
    {
        children = children.FindAll(x => x != node);
    }

    public void RemoveAllChildren()
    {
        children = [];
    }

    public void RemoveFromParent()
    {
        parent?.RemoveChild(this);
    }

    public void AddCountdownTimer(double second, EventHandler eventHandler)
    {

    }

    public virtual void Initialize()
    {
        foreach (var countdownTimer in countdownTimers)
        {
            countdownTimer.Key.Completed += countdownTimer.Value;
        }

        foreach (var continuousClock in continuousClocks)
        {
            continuousClock.Key.Tick += continuousClock.Value;
        }

        foreach (var tweenerAction in tweenerActions)
        {
            var t = new Tweener();
            tweeners.Add(t);
            tweenerAction(t);
        }

        foreach (var child in children)
        {
            foreach (var tweenerAction in child.tweenerActions)
            {
                var t = new Tweener();
                child.tweeners.Add(t);
                tweenerAction(t);
            }

        }
    }

    public virtual void Update(GameTime gameTime)
    {
        CheckHoverState();

        foreach (var countdownTimer in countdownTimers)
        {
            countdownTimer.Key.Update(gameTime);
        }



        foreach (var continuousClock in continuousClocks)
        {
            continuousClock.Key.Update(gameTime);
        }

        foreach (var child in children)
        {
            child.parent = this;
            child.Update(gameTime);

            foreach (var tweener in child.tweeners)
            {
                tweener.Update(gameTime.GetElapsedSeconds());
            }

        }

        // foreach (var component in components)
        // {
        //     component.belong = this;
        //     component.Update(gameTime);
        // }
    }

    public virtual void Draw()
    {
        foreach (var node in children)
        {
            node.Draw();
        }
    }

    public virtual Vector2 GetSize()
    {
        return new();
    }

    private void CheckHoverState()
    {
        if (MyGame.IsActive)
        {
            if (Rectangle.Contains(MyGame.MousePos))
            {
                if (!isHover)
                {
                    isHover = true;
                    OnMouseEnter.Invoke(this, MyGame.MousePos);
                }

                if (MyGame.MouseState.WasButtonPressed(MouseButton.Left))
                {
                    OnMouseDown.Invoke(this, MyGame.MousePos);
                }

                if (MyGame.MouseState.WasButtonReleased(MouseButton.Left))
                {
                    OnMouseUp.Invoke(this, MyGame.MousePos);
                    OnClick.Invoke(this, MyGame.MousePos);
                }

                OnMouseMove.Invoke(this, MyGame.MousePos);
            }
            else
            {
                if (isHover)
                {
                    isHover = false;
                    OnMouseLeave.Invoke(this, MyGame.MousePos);
                }

                if (MyGame.MouseState.WasButtonPressed(MouseButton.Left))
                {
                    OnOutSideClick.Invoke(this, MyGame.MousePos);
                }
            }
        }

    }
}