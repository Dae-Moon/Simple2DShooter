
namespace Simple2DShooter;

internal class Bullet : GameComponent
{
    private Vector2 _position;

    private readonly Vector2 _direction;
    private readonly Weapon _weapon;

    public Bullet(Vector2 position, Vector2 direction, Weapon weapon)
    {
        _position = position;
        _direction = Vector2.Normalize(direction);
        _weapon = weapon;
    }

    public override void Update()
    {
        _position += _direction * _weapon.bulletSpeed * IResources.frameTime;

        var rect = Window.Instance.Bounds;
        if (_position.X < 0 || _position.Y < 0 ||
            _position.X > rect.Width || _position.Y > rect.Height)
            Destroy();
        else
            CheckHit();
    }

    private void CheckHit()
    {
        foreach (var comp in IResources.gameComponents.ToList())
        {
            if (comp != null && comp is Enemy)
            {
                var enemy = comp as Enemy;
                float distanceX = _position.X - enemy.position.X;
                float distanceY = _position.Y - enemy.position.Y;
                float radiusSum = enemy.radius + _weapon.bulletSize;
                if (distanceX * distanceX + distanceY * distanceY <= radiusSum * radiusSum)
                {
                    enemy.TakeDamage(_weapon);
                    Destroy();
                }
            }
        }
    }

    public override void Draw(Graphics graphics)
    {
        graphics.FillEllipse(Brushes.Orange, _position.X - _weapon.bulletSize, _position.Y - _weapon.bulletSize, _weapon.bulletSize * 2f, _weapon.bulletSize * 2f);
    }
}
