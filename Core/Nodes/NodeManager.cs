using Microsoft.Xna.Framework;

class NodeManager(Node belong)
{
    public List<Node> children = [];

    public void AddChild(Node node)
    {
        node.parent = belong;
        children.Add(node);
    }

    public List<T> GetChildOfType<T>() where T : Node
    {
        return children.OfType<T>().ToList();
    }

    public T GetChildByTag<T>(string tag) where T : Node
    {
        return children.OfType<T>().FirstOrDefault(x => x.tag == tag);
    }

    public T GetFirstChildOfType<T>() where T : Node
    {
        return children.OfType<T>().FirstOrDefault();
    }

    public void RemoveChildByTag(string tag)
    {
        children.RemoveAll(x => x.tag == tag);
    }

    public void RemoveChild(Node node)
    {
        children.RemoveAll(x => x == node);
    }

    public void RemoveAllChildren()
    {
        children.Clear();
    }

    public virtual void Initialize()
    {
        foreach (var node in children)
        {
            node.Initialize();
        }
    }

    public virtual void Update(GameTime gameTime)
    {
        foreach (var child in children)
        {
            child.parent = belong;
            child.Update(gameTime);
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