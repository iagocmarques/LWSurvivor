namespace Project.Gameplay.Combat
{
    /// <summary>
    /// Implementado por hurtboxes que podem receber golpes (dummy, inimigos, etc.).
    /// </summary>
    public interface ICombatHurtbox
    {
        /// <summary>
        /// Retorna true se o golpe foi aceito (ex.: não estava invulnerável).
        /// </summary>
        bool ReceiveHit(in CombatHitInfo hit);
    }
}
