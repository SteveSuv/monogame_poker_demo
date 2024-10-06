
using Microsoft.Xna.Framework;

class Node
{
    public List<Node> children = [];
    public List<Component> components = [];
    public Node parent;
    public string tag;
    public TransformComponent transform => GetComponent<TransformComponent>();

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

    public void RemoveAllChildren()
    {
        children = [];
    }

    public virtual void Update(GameTime gameTime)
    {
        foreach (var child in children)
        {
            child.Update(gameTime);
            child.parent = this;
        }
        foreach (var component in components)
        {
            component.Update(gameTime);
            component.belong = this;
        }
    }

    public virtual void Draw()
    {
        foreach (var node in children)
        {
            node.Draw();
        }
    }
}