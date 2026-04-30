using System.Collections.Generic;
using Project.Gameplay.Audio;
using Project.Gameplay.Combat;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2ItrProcessor
    {
        private static readonly Collider2D[] OverlapScratch = new Collider2D[16];

        private Lf2StateMachine _sm;
        private Transform _owner;
        private LayerMask _hurtMask;
        private LayerMask _itemMask;
        private LayerMask _projectileMask;
        private Lf2GrabProcessor _grabProcessor;
        private Lf2WeaponManager _weaponManager;
        private System.Action _onHit;

        private int _arestRemaining;

        private readonly Dictionary<Collider2D, int> _vrestMap = new();
        private readonly List<Collider2D> _vrestToRemove = new();

        private ContactFilter2D _hurtFilter;
        private ContactFilter2D _itemFilter;
        private ContactFilter2D _projectileFilter;
        private bool _filtersDirty;

        public void Initialize(Lf2StateMachine sm, Transform owner, LayerMask hurtMask)
        {
            _sm = sm;
            _owner = owner;
            _hurtMask = hurtMask;
            _arestRemaining = 0;
            _vrestMap.Clear();
            _filtersDirty = true;
        }

        public void SetItemMask(LayerMask itemMask)
        {
            _itemMask = itemMask;
            _filtersDirty = true;
        }

        public void SetWeaponManager(Lf2WeaponManager weaponManager)
        {
            _weaponManager = weaponManager;
        }

        public void SetGrabProcessor(Lf2GrabProcessor grabProcessor)
        {
            _grabProcessor = grabProcessor;
        }

        public void SetOnHit(System.Action callback)
        {
            _onHit = callback;
        }

        public void SetProjectileMask(LayerMask mask)
        {
            _projectileMask = mask;
            _filtersDirty = true;
        }

        private void RebuildFilters()
        {
            _hurtFilter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = _hurtMask,
                useTriggers = true
            };
            _itemFilter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = _itemMask,
                useTriggers = true
            };
            if (_projectileMask.value != 0)
            {
                _projectileFilter = new ContactFilter2D
                {
                    useLayerMask = true,
                    layerMask = _projectileMask,
                    useTriggers = true
                };
            }
            _filtersDirty = false;
        }

        public void Tick()
        {
            if (_sm?.CurrentFrame == null) return;

            if (_filtersDirty) RebuildFilters();

            DecrementVrest();

            if (_arestRemaining > 0) _arestRemaining--;

            var frame = _sm.CurrentFrame;
            var itrs = frame.Itrs;

            if (itrs == null || itrs.Length == 0) return;
            if (_arestRemaining > 0) return;

            bool facingRight = _sm.FacingRight;
            var ownerPos = _owner.position;

            for (int i = 0; i < itrs.Length; i++)
            {
                var itr = itrs[i];

                if (itr.Kind == Lf2ItrKind.PickupItem)
                {
                    ProcessPickup(itr, ownerPos, facingRight);
                    continue;
                }

                if (itr.Kind != Lf2ItrKind.Hit && itr.Kind != Lf2ItrKind.GrabBody && itr.Kind != Lf2ItrKind.Throw)
                    continue;

                var rect = itr.Rect;
                float scale = Lf2StateMachine.PixelToUnit;

                var mirroredCenter = Lf2MirrorUtil.MirrorLocalOffset(
                    new Vector2(rect.x + rect.width * 0.5f, rect.y + rect.height * 0.5f),
                    facingRight);

                var center = new Vector2(
                    ownerPos.x + mirroredCenter.x * scale,
                    ownerPos.y + mirroredCenter.y * scale
                );
                var size = new Vector2(rect.width * scale, rect.height * scale);

                var filter = _hurtFilter;

                int count = Physics2D.OverlapBox(center, size, 0f, filter, OverlapScratch);

                for (int j = 0; j < count; j++)
                {
                    var col = OverlapScratch[j];
                    if (col == null) continue;
                    if (col.gameObject == _owner.gameObject) continue;

                    if (_vrestMap.ContainsKey(col)) continue;

                    if (itr.Kind == Lf2ItrKind.GrabBody)
                    {
                        ProcessGrab(col, itr);
                    }
                    else
                    {
                        ProcessHit(col, itr, facingRight);
                        _onHit?.Invoke();
                    }
                }

                if (_projectileMask.value != 0 && itr.Kind == Lf2ItrKind.Hit)
                {
                    int projCount = Physics2D.OverlapBox(center, size, 0f, _projectileFilter, OverlapScratch);
                    for (int j = 0; j < projCount; j++)
                    {
                        var col = OverlapScratch[j];
                        if (col == null || col.gameObject == _owner.gameObject) continue;

                        var otherProjectile = col.GetComponent<Lf2Projectile>();
                        if (otherProjectile != null && otherProjectile.IsActive)
                        {
                            otherProjectile.OnProjectileHit();
                            _onHit?.Invoke();
                        }
                    }
                }
            }
        }

        private void ProcessPickup(Lf2ItrData itr, Vector2 ownerPos, bool facingRight)
        {
            var rect = itr.Rect;
            float scale = Lf2StateMachine.PixelToUnit;

            var mirroredCenter = Lf2MirrorUtil.MirrorLocalOffset(
                new Vector2(rect.x + rect.width * 0.5f, rect.y + rect.height * 0.5f),
                facingRight);

            var center = new Vector2(
                ownerPos.x + mirroredCenter.x * scale,
                ownerPos.y + mirroredCenter.y * scale
            );
            var size = new Vector2(rect.width * scale, rect.height * scale);

            int count = Physics2D.OverlapBox(center, size, 0f, _itemFilter, OverlapScratch);
            for (int j = 0; j < count; j++)
            {
                var col = OverlapScratch[j];
                if (col == null) continue;

                var item = col.GetComponent<Lf2Item>();
                if (item != null)
                {
                    item.ApplyTo(_owner.gameObject);
                    break;
                }

                if (_weaponManager == null || _weaponManager.HasWeapon) continue;

                var weapon = col.GetComponent<Lf2Weapon>();
                if (weapon == null || !weapon.CanPickup) continue;

                _weaponManager.TryPickup();
                break;
            }
        }

        private void ProcessHit(Collider2D target, Lf2ItrData itr, bool facingRight)
        {
            if (!target.TryGetComponent<ICombatHurtbox>(out var hurtbox)) return;

            var rawKnockback = new Vector2(
                itr.Dvx * Lf2StateMachine.PixelToUnit,
                itr.Dvy * Lf2StateMachine.PixelToUnit
            );
            var knockback = Lf2MirrorUtil.MirrorKnockback(rawKnockback, facingRight);

            int hitStop = HitStopFromInjury(itr.Injury);
            float shake = ShakeFromInjury(itr.Injury);
            StatusEffect effect = ConvertEffect(itr.Effect);

            var hitInfo = new CombatHitInfo(
                _owner.gameObject,
                itr.Injury,
                knockback,
                CombatAttackId.None,
                hitStop,
                shake,
                itr.Kind == Lf2ItrKind.GrabBody,
                itr.Bdefend,
                itr.Fall,
                effect
            );

            hurtbox.ReceiveHit(in hitInfo);

            var audio = Lf2AudioManager.Instance;
            if (audio != null)
                audio.PlayHitSound((int)itr.Kind, itr.Injury);

            _arestRemaining = itr.Arest;

            if (itr.Vrest > 0)
                _vrestMap[target] = itr.Vrest;
        }

        private void ProcessGrab(Collider2D target, Lf2ItrData itr)
        {
            if (_grabProcessor == null || _grabProcessor.IsGrabbing) return;

            var victimSm = target.GetComponentInParent<PlayerHsmController>();
            if (victimSm == null || !victimSm.HasLf2Sm) return;

            if (!_grabProcessor.TryGrab(victimSm.Lf2Sm, victimSm.transform)) return;

            _arestRemaining = itr.Arest;

            if (itr.Vrest > 0)
                _vrestMap[target] = itr.Vrest;
        }

        private void DecrementVrest()
        {
            _vrestToRemove.Clear();

            var e = _vrestMap.GetEnumerator();
            while (e.MoveNext())
            {
                int remaining = e.Current.Value - 1;
                if (remaining <= 0)
                    _vrestToRemove.Add(e.Current.Key);
                else
                    _vrestMap[e.Current.Key] = remaining;
            }

            for (int i = 0; i < _vrestToRemove.Count; i++)
                _vrestMap.Remove(_vrestToRemove[i]);
        }

        private static StatusEffect ConvertEffect(Lf2EffectType effect)
        {
            return effect switch
            {
                Lf2EffectType.Bleed => StatusEffect.Blood,
                Lf2EffectType.Fire => StatusEffect.Burn,
                Lf2EffectType.Ice => StatusEffect.Freeze,
                _ => StatusEffect.None
            };
        }

        private static int HitStopFromInjury(int injury)
        {
            if (injury >= 30) return 5;
            if (injury >= 20) return 4;
            if (injury >= 10) return 3;
            return 2;
        }

        private static float ShakeFromInjury(int injury)
        {
            if (injury >= 30) return 0.25f;
            if (injury >= 20) return 0.20f;
            if (injury >= 10) return 0.15f;
            return 0.08f;
        }
    }
}
