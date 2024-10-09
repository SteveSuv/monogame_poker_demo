
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input;

class Node
{
    public List<Node> children = [];
    public List<Component> components = [];
    public Node parent;
    public string tag;
    public TransformComponent Transform => GetComponent<TransformComponent>();
    public Vector2 Size => GetSize();
    public Vector2 OriginOffset => Transform.origin * Size;
    public RectangleF Rectangle => new(Transform.WorldPosition - OriginOffset, Size);
    private bool isHover = false;
    public EventHandler<Vector2> OnHover = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnClick = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnLeave = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnOutSideClick = (object sender, Vector2 mousePos) => { };


    public Node()
    {
        AddComponent(new TransformComponent());
    }

    public Node AddComponent(Component component)
    {
        component.belong = this;
        components.Add(component);
        return this;
    }

    public Node AddChild(Node node)
    {
        node.parent = this;
        children.Add(node);
        return this;
    }

    public T GetComponent<T>() where T : Component
    {
        return components.Find(x => x is T) as T;
    }


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

    public virtual void Update(GameTime gameTime)
    {

        CheckHoverState();

        foreach (var child in children)
        {
            child.Update(gameTime);

            if (child.parent != this)
            {
                child.parent = this;
            }
        }

        foreach (var component in components)
        {
            component.Update(gameTime);

            if (component.belong != this)
            {
                component.belong = this;
            }
        }
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
                    OnHover.Invoke(this, MyGame.MousePos);
                }


                if (MyGame.MouseState.WasButtonPressed(MouseButton.Left))
                {
                    OnClick.Invoke(this, MyGame.MousePos);
                }
            }
            else
            {
                if (isHover)
                {
                    isHover = false;
                    OnLeave.Invoke(this, MyGame.MousePos);

                }

                if (MyGame.MouseState.WasButtonPressed(MouseButton.Left))
                {
                    OnOutSideClick.Invoke(this, MyGame.MousePos);
                }
            }
        }

    }
}