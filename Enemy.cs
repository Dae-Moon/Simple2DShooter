﻿
namespace Simple2DShooter;

internal class Enemy : GameComponent
{
    private int _health;
    private float _knockback;

    public Vector2 position, direction;
    public float speed, radius;

    public Enemy(int health)
    {
        _health = health;
    }

    public override void Update()
    {
        var dir = Player.LocalPlayer.position - position;

        if (dir.Length() <= Player.LocalPlayer.radius + radius)
        {
            Destroy();
            return;
        }

        if (_knockback > 1f)
        {
            position += -direction * _knockback * IResources.frameTime;
            _knockback -= speed * IResources.frameTime;
        }
        else
            direction = Vector2.Normalize(dir);

        position += direction * speed * IResources.frameTime;
    }

    public override void Draw(Graphics graphics)
    {
        graphics.FillEllipse(Brushes.Red, position.X - radius, position.Y - radius, radius * 2, radius * 2);

        if (_health > 0)
        {
            var y = radius * 2f;
            var x = y * 2 * (_health / 100f);
            int g = Convert.ToInt32(_health * 2.55f);
            graphics.DrawLine(new(Color.FromArgb(255 - g, g, 0), 3f), position.X - x, position.Y - y, position.X + x, position.Y - y);
        }
    }

    public bool TakeDamage(Weapon weapon)
    {
        _health -= weapon.damageDistance <= 0 ? weapon.damage : Convert.ToInt32(weapon.damage / (Vector2.Distance(Player.LocalPlayer.position, position) / weapon.damageDistance));
        _knockback = weapon.knockback;

        if (_health <= 0)
        {
            Destroy();
            weapon.killCount++;
            return true;
        }

        return false;
    }
}