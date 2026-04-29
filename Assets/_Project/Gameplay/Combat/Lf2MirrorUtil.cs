using UnityEngine;

namespace Project.Gameplay.Combat
{
    public static class Lf2MirrorUtil
    {
        private const float Lf2FrameWidth = 80f;

        /// <summary>
        /// Espelha retângulo (x, y, w, h) no eixo X em relação à largura do frame.
        /// Usar para bdy e itr quando facing left.
        /// </summary>
        public static Rect MirrorRectX(Rect r)
        {
            return new Rect(Lf2FrameWidth - (r.x + r.width), r.y, r.width, r.height);
        }

        /// <summary>
        /// Espelha ponto (x, y) no eixo X em relação à largura do frame.
        /// Usar para opoint/wpoint quando facing left.
        /// </summary>
        public static Vector2 MirrorPointX(Vector2 p)
        {
            return new Vector2(Lf2FrameWidth - p.x, p.y);
        }

        /// <summary>
        /// Aplica facing ao offset local de hitbox (centro do retângulo).
        /// Converte offset de "sempre positivo" para "negativo quando facing left".
        /// </summary>
        public static Vector2 MirrorLocalOffset(Vector2 offset, bool facingRight)
        {
            return facingRight
                ? offset
                : new Vector2(-offset.x, offset.y);
        }

        /// <summary>
        /// Espelha knockback quando facing left (inverte componente X).
        /// </summary>
        public static Vector2 MirrorKnockback(Vector2 knockback, bool facingRight)
        {
            return facingRight
                ? knockback
                : new Vector2(-knockback.x, knockback.y);
        }
    }
}
