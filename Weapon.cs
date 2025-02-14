namespace Simple2DShooter;

internal class Weapon
{
    private static DateTime _lastShootTime = DateTime.Now;

    public float cooldown;
    public float knockback;
    public float bulletSpeed;
    public float bulletSize;
    public float damageDistance;
    public int damage, killCount;

    protected virtual void OnShoot(Vector2 position) { }

    public void Shoot(Vector2 position)
    {
        var elapsedTime = (DateTime.Now - _lastShootTime).TotalSeconds;
        if (elapsedTime >= cooldown)
        {
            _lastShootTime = DateTime.Now;
            OnShoot(position);
        }
    }
}
