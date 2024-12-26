namespace Simple2DShooter;

internal class GameComponent
{
    public GameComponent()
    {
        IResources.gameComponents.Add(this);
    }

    ~GameComponent()
    {
        Destroy();
    }

    public virtual void Initialize() { }
    public virtual void Update() { }
    public virtual void Draw(Graphics graphics) { }

    public void Destroy()
    {
        IResources.gameComponents.Remove(this);
    }
}
