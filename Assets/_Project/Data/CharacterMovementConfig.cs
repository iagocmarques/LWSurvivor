using System;
using UnityEngine;

namespace Project.Data
{
    [Serializable]
    public struct CharacterMovementConfig
    {
        [Header("Walk")]
        public float walkSpeed;
        public float walkAccel;
        
        [Header("Run")]
        public float runSpeed;
        public float runAccel;
        
        [Header("Jump")]
        public float jumpPower;
        public float jumpForwardSpeed;
        public float gravity;
        
        [Header("Dash")]
        public float dashSpeed;
        public float dashDuration;
        
        [Header("Reactive Combat")]
        public float guardBreakThreshold;
        public float airLaunchThreshold;

        [Header("Other")]
        public float defendSpeedMultiplier;
        public int lyingDurationTicks;
        public int invulnOnGetUpTicks;
        
        public static CharacterMovementConfig FromLf2Movement(System.Collections.Generic.Dictionary<string, float> movement)
        {
            var cfg = new CharacterMovementConfig();
            if (movement == null) return cfg;
            
            cfg.walkSpeed = GetValue(movement, "walk_speed", 5f);
            cfg.walkAccel = GetValue(movement, "walk_acc", 0.5f);
            cfg.runSpeed = GetValue(movement, "run_speed", 8f);
            cfg.runAccel = GetValue(movement, "run_acc", 0.5f);
            cfg.jumpPower = GetValue(movement, "jump_height", 12f);
            cfg.jumpForwardSpeed = GetValue(movement, "jump_distance", 5f);
            cfg.gravity = GetValue(movement, "gravity", 0.5f);
            cfg.dashSpeed = GetValue(movement, "dash_speed", 10f);
            cfg.dashDuration = GetValue(movement, "dash_duration", 0.3f);
            cfg.guardBreakThreshold = 15f;
            cfg.airLaunchThreshold = 5f;
            cfg.defendSpeedMultiplier = 0.5f;
            cfg.lyingDurationTicks = 60;
            cfg.invulnOnGetUpTicks = 30;
            return cfg;
        }
        
        private static float GetValue(System.Collections.Generic.Dictionary<string, float> dict, string key, float fallback)
        {
            return dict.TryGetValue(key, out var v) ? v : fallback;
        }
    }
}
