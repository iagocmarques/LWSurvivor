namespace Project.Gameplay.LF2
{
    public enum Lf2State
    {
        Standing = 0,
        Walking = 1,
        Running = 2,
        Attacking = 3,
        Jumping = 4,
        Dashing = 5,
        Falling = 6,
        Defending = 7,
        BrokenDefend = 8,
        Catching = 9,
        Caught = 10,
        Injured = 11,
        FallingAlt = 12,
        Ice = 13,
        Lying = 14,
        Fire = 15,

        Projectile = 300,
    }

    public enum Lf2ItrKind
    {
        Hit = 0,
        GrabBody = 1,
        GrabWeapon = 2,
        Throw = 3,
        PickupItem = 4,
        Defend = 5,
    }

    public enum Lf2EffectType
    {
        None = 0,
        Bleed = 1,
        Fire = 2,
        Ice = 3,
    }
}
