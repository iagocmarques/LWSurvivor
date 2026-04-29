namespace Project.Gameplay.Combat
{
    /// <summary>
    /// Identidade lógica do ataque (buffer, cancel e hitbox usam o mesmo enum).
    /// </summary>
    public enum CombatAttackId : byte
    {
        None = 0,
        Jab = 1,
        Launcher = 2,
        DashAttack = 3
    }
}
