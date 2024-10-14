using Microsoft.Xna.Framework;

class ComponentManager(Node belong)
{
    public List<Component> components = [];

    public ComponentManager AddComponent(Component component)
    {
        component.belong = belong;
        components.Add(component);
        return this;
    }

    public List<T> GetComponentsOfType<T>() where T : Component
    {
        return components.OfType<T>().ToList();
    }

    public T GetComponentByTag<T>(string tag) where T : Component
    {
        return components.OfType<T>().FirstOrDefault(x => x.tag == tag);
    }

    public T GetFirstComponentOfType<T>() where T : Component
    {
        return components.OfType<T>().FirstOrDefault();
    }

    public void RemoveComponentByTag(string tag)
    {
        components.RemoveAll(x => x.tag == tag);
    }

    public void RemoveComponent(Component component)
    {
        components.RemoveAll(x => x == component);
    }

    public void RemoveAllComponent()
    {
        components.Clear();
    }

    public virtual void Initialize()
    {
        foreach (var component in components)
        {
            component.Initialize();
        }
    }

    public virtual void Update(GameTime gameTime)
    {
        foreach (var component in components)
        {
            component.belong = belong;
            component.Update(gameTime);
        }
    }
}