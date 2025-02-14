
using System.Diagnostics.Eventing.Reader;

namespace Simple2DShooter;

internal class Player : GameComponent
{
    private float _powerSpeed = 1f;
    private bool _powerActive = true;

    public static Player LocalPlayer { get; private set; }

    public int health = 100;
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
        if (health <= 0)
            Window.Instance!.isRunning = false;

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

        if (health > 0)
        {
            var y = radius * 2f;
            var x = y * 2 * (health / 100f);
            int g = Convert.ToInt32(health * 2.55f);
            graphics.DrawLine(new(Color.FromArgb(255 - g, g, 0), 3f), position.X - x, position.Y - y, position.X + x, position.Y - y);
        }

        graphics.DrawLine(new Pen(Color.Green, 10f), (Window.Instance!.ClientSize.ToVector() - new Vector2(10f, 5f)).ToPointF(), new(Window.Instance!.ClientSize.Width - 10f, Window.Instance!.ClientSize.Height - 5f - (200f * _powerSpeed)));
        graphics.DrawString($"{(int)MathF.Ceiling(_powerSpeed * 100f)}%", Window.Instance!.Font, Brushes.White, Window.Instance!.ClientSize.Width - 20f, Window.Instance!.ClientSize.Height - 5f, new() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far });
    }

    private void PlayerInput()
    {
        float currentSpeed = speed;

        if (_powerActive)
        {
            if (Input.IsKey(Keys.ShiftKey))
            {
                currentSpeed *= 1.5f;

                if (_powerSpeed > 0f)
                    _powerSpeed -= 0.5f * IResources.frameTime;
                else
                    _powerActive = false;
            }
        }
        else
        {
            if (_powerSpeed < 0.25f)
                currentSpeed *= 0.75f;

            if (_powerSpeed < 1f)
                _powerSpeed += 0.25f * IResources.frameTime;
            else
                _powerActive = true;
        }

        _powerSpeed = Math.Clamp(_powerSpeed, 0f, 1f);

        if (Input.IsKey(Keys.W))
            position.Y -= currentSpeed * IResources.frameTime;
        if (Input.IsKey(Keys.A))
            position.X -= currentSpeed * IResources.frameTime;
        if (Input.IsKey(Keys.S))
            position.Y += currentSpeed * IResources.frameTime;
        if (Input.IsKey(Keys.D))
            position.X += currentSpeed * IResources.frameTime;

        if (Input.IsMouse(MouseButtons.Left))
            Inventory.CurrentWeapon.Shoot(position + direction * (radius * 2f));
    }
}
