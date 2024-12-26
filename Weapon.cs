namespace Simple2DShooter;

public enum WeaponType
{
    Knife,
    Gun,
    Shotgun
}

internal class Weapon
{
    private static DateTime _lastShootTime = DateTime.Now;

    public WeaponType weaponType;
    public float cooldown;
    public float knockback;
    public float bulletSpeed;
    public float bulletSize;
    public float damageDistance;
    public int damage, killCount;

    public void Shoot(Vector2 position)
    {
        if (!CooldownPassed())
            return;

        if (weaponType == WeaponType.Knife)
        {
            var pos = Input.MousePosition.ToVector();
            var dir = Utils.random.NextSingle(0f, 360f).ToDirection() * 15f;
            new KnifeEffect(pos + dir, pos - dir, this);
        }
        else if (weaponType == WeaponType.Shotgun)
        {
            var angle = (Input.MousePosition.ToVector() - position).ToAngle();

            for (int i = 0; i < 4; i++)
                new Bullet(position, Utils.random.NextSingle(angle - 2.5f, angle + 2.5f).ToDirection(), this);
            
            for (int i = 0; i < 3; i++)
                new Bullet(position, Utils.random.NextSingle(angle - 15f, angle + 15f).ToDirection(), this);

            for (int i = 0; i < 2; i++)
                new Bullet(position, Utils.random.NextSingle(angle - 35f, angle + 35f).ToDirection(), this);
        }
        else
            new Bullet(position, Input.MousePosition.ToVector() - position, this);
    }

    private bool CooldownPassed()
    {
        var elapsedTime = (DateTime.Now - _lastShootTime).TotalSeconds;
        if (elapsedTime >= cooldown)
        {
            _lastShootTime = DateTime.Now;
            return true;
        }

        return false;
    }
}
