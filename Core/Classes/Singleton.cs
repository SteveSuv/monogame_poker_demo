class Singleton
{
    private static readonly Singleton _instance = new();

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            return _instance;
        }
    }
}
