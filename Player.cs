
namespace Simple2DShooter;

internal class Player : GameComponent
{
    public static Player LocalPlayer { get; private set; }

    public Vector2 position, direction;
    public float speed, radius;

    public Inventory Inventory { get; private set; }

    public Player()
    {
        LocalPlayer = this;
        Inventory = new();
    }

    public override void Update()
    {
        direction = Vector2.Normalize(Input.MousePosition.ToVector() - position);
        
        PlayerInput();

        base.Update();
    }

    public override void Draw(Graphics graphics)
    {
        graphics.DrawEllipse(Pens.Green, position.X - radius, position.Y - radius, radius * 2, radius * 2);

        var start = position + direction * radius;
        var end = start + direction * radius;
        graphics.DrawLine(Pens.Red, start.ToPointF(), end.ToPointF());
    }

    private void PlayerInput()
    {
        if (Input.IsKey(Keys.W))
            position.Y -= speed * IResources.frameTime;
        if (Input.IsKey(Keys.A))
            position.X -= speed * IResources.frameTime;
        if (Input.IsKey(Keys.S))
            position.Y += speed * IResources.frameTime;
        if (Input.IsKey(Keys.D))
            position.X += speed * IResources.frameTime;

        if (Input.IsMouse(MouseButtons.Left))
            Inventory.CurrentWeapon.Shoot(position + direction * (radius * 2f));
    }
}
