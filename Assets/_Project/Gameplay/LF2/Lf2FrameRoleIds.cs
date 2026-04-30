using System.Collections.Generic;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2FrameRoleIds
    {
        public int Standing = 0;
        public int Walking = 1;
        public int Running = 2;
        public int Jump = 210;
        public int Defend = 100;
        public int Catching = 150;
        public int Caught = 155;
        public int GrabAttack = 160;
        public int Throw = 170;
        public int Lying = 230;

        public int AttackNeutral = -1;
        public int AttackForward = -1;
        public int AttackBack = -1;

        public int EnergyBlast = -1;
        public int Shrafe = -1;
        public int LeapAttack = -1;
        public int DragonPunch = -1;
        public int DownAttack = -1;
        public int DownJump = -1;

        public int EnergyBlastMpCost = 25;
        public int ShrafeMpCost = 20;
        public int LeapAttackMpCost = 15;
        public int DragonPunchMpCost = 30;
        public int DownAttackMpCost = 20;
        public int DownJumpMpCost = 15;

        private static int ReadHitProp(Lf2FrameData f, string key)
        {
            if (f.ExtraProps != null && f.ExtraProps.TryGetValue(key, out var v)
                && int.TryParse(v, out var id) && id > 0)
                return id;
            return -1;
        }

        public static Lf2FrameRoleIds BuildFromCharacterData(Lf2CharacterData character)
        {
            var roles = new Lf2FrameRoleIds();
            if (character?.Frames == null || character.Frames.Count == 0)
                return roles;

            var frames = character.Frames;

            foreach (var kv in frames)
            {
                var f = kv.Value;
                switch (f.State)
                {
                    case Lf2State.Walking:
                        if (roles.Walking == 1 || !frames.ContainsKey(roles.Walking))
                            roles.Walking = kv.Key;
                        break;
                    case Lf2State.Running:
                        if (roles.Running == 2 || !frames.ContainsKey(roles.Running))
                            roles.Running = kv.Key;
                        break;
                    case Lf2State.Jumping:
                        if (roles.Jump == 210 || !frames.ContainsKey(roles.Jump))
                            roles.Jump = kv.Key;
                        break;
                    case Lf2State.Defending:
                        if (roles.Defend == 100 || !frames.ContainsKey(roles.Defend))
                            roles.Defend = kv.Key;
                        break;
                    case Lf2State.Catching:
                        if (roles.Catching == 150 || !frames.ContainsKey(roles.Catching))
                            roles.Catching = kv.Key;
                        break;
                    case Lf2State.Caught:
                        if (roles.Caught == 155 || !frames.ContainsKey(roles.Caught))
                            roles.Caught = kv.Key;
                        break;
                    case Lf2State.Lying:
                        if (roles.Lying == 230 || !frames.ContainsKey(roles.Lying))
                            roles.Lying = kv.Key;
                        break;
                }

                if (f.State == Lf2State.Attacking && f.Itrs != null && f.Itrs.Length > 0)
                {
                    var name = f.Name ?? "";
                    if (name.IndexOf("super_punch", System.StringComparison.OrdinalIgnoreCase) >= 0
                        || name.IndexOf("launcher", System.StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (roles.AttackForward < 0)
                            roles.AttackForward = kv.Key;
                    }
                    else if (name.IndexOf("jump_attack", System.StringComparison.OrdinalIgnoreCase) >= 0
                             || name.IndexOf("dash_attack", System.StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (roles.AttackBack < 0)
                            roles.AttackBack = kv.Key;
                    }
                    else if (roles.AttackNeutral < 0)
                    {
                        roles.AttackNeutral = kv.Key;
                    }
                }

                if (f.Opoints != null && f.Opoints.Length > 0)
                {
                    var name = f.Name ?? "";
                    if (name.IndexOf("ball1", System.StringComparison.OrdinalIgnoreCase) >= 0
                        && roles.EnergyBlast < 0)
                        roles.EnergyBlast = kv.Key;
                    else if (name.IndexOf("ball2", System.StringComparison.OrdinalIgnoreCase) >= 0
                             && roles.Shrafe < 0)
                        roles.Shrafe = kv.Key;
                    else if (name.IndexOf("ball3", System.StringComparison.OrdinalIgnoreCase) >= 0
                             && roles.LeapAttack < 0)
                        roles.LeapAttack = kv.Key;
                    else if (name.IndexOf("ball4", System.StringComparison.OrdinalIgnoreCase) >= 0
                             && roles.DragonPunch < 0)
                        roles.DragonPunch = kv.Key;
                }
            }

            DetectSpecialsFromHitBindings(roles, frames);

            return roles;
        }

        private static void DetectSpecialsFromHitBindings(
            Lf2FrameRoleIds roles, Dictionary<int, Lf2FrameData> frames)
        {
            bool needEnergyBlast = roles.EnergyBlast < 0;
            bool needShrafe = roles.Shrafe < 0;
            bool needLeapAttack = roles.LeapAttack < 0;
            bool needDragonPunch = roles.DragonPunch < 0;
            bool needDownAttack = roles.DownAttack < 0;
            bool needDownJump = roles.DownJump < 0;

            if (!needEnergyBlast && !needShrafe && !needLeapAttack
                && !needDragonPunch && !needDownAttack && !needDownJump)
                return;

            foreach (var kv in frames)
            {
                var f = kv.Value;
                if (f.State != Lf2State.Standing && f.State != Lf2State.Walking)
                    continue;

                if (needEnergyBlast)
                {
                    int id = ReadHitProp(f, "hit_Fa");
                    if (id > 0 && frames.ContainsKey(id))
                    {
                        roles.EnergyBlast = id;
                        needEnergyBlast = false;
                    }
                }

                if (needShrafe)
                {
                    int id = ReadHitProp(f, "hit_Fj");
                    if (id > 0 && frames.ContainsKey(id))
                    {
                        roles.Shrafe = id;
                        needShrafe = false;
                    }
                }

                if (needLeapAttack)
                {
                    int id = ReadHitProp(f, "hit_Uj");
                    if (id > 0 && frames.ContainsKey(id))
                    {
                        roles.LeapAttack = id;
                        needLeapAttack = false;
                    }
                }

                if (needDragonPunch)
                {
                    int id = ReadHitProp(f, "hit_Ua");
                    if (id > 0 && frames.ContainsKey(id))
                    {
                        roles.DragonPunch = id;
                        needDragonPunch = false;
                    }
                }

                if (needDownAttack)
                {
                    int id = ReadHitProp(f, "hit_Da");
                    if (id > 0 && frames.ContainsKey(id))
                    {
                        roles.DownAttack = id;
                        needDownAttack = false;
                    }
                }

                if (needDownJump)
                {
                    int id = ReadHitProp(f, "hit_Dj");
                    if (id > 0 && frames.ContainsKey(id))
                    {
                        roles.DownJump = id;
                        needDownJump = false;
                    }
                }

                if (!needEnergyBlast && !needShrafe && !needLeapAttack
                    && !needDragonPunch && !needDownAttack && !needDownJump)
                    break;
            }

            if (roles.DragonPunch < 0 && roles.DownAttack > 0)
                roles.DragonPunch = roles.DownAttack;
        }
    }
}
