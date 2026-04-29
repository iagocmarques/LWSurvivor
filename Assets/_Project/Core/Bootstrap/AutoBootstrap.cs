using Project.Core.Tick;
using UnityEngine;

namespace Project.Core.Bootstrap
{
    public static class AutoBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Install()
        {
            if (Exists<Bootstrapper>())
                return;

            var go = new GameObject("_AutoBootstrap");
            Object.DontDestroyOnLoad(go);

            go.AddComponent<Bootstrapper>();
            go.AddComponent<FixedTickInstaller>();
        }

        private static bool Exists<T>() where T : Object
        {
            var all = Resources.FindObjectsOfTypeAll<T>();
            return all != null && all.Length > 0;
        }
    }
}