namespace Simple2DShooter.Weapons;

internal class Gun : Weapon
{
    protected override void OnShoot(Vector2 position)
    {
        new Bullet(position, Input.MousePosition.ToVector() - position, this);
    }
}
