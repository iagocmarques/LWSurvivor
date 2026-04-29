using System.Collections.Generic;

namespace LF2Importer.EditorTools
{
    public sealed class Lf2OidResolver
    {
        private readonly Dictionary<int, Lf2ObjectEntry> _byId = new Dictionary<int, Lf2ObjectEntry>();

        public Lf2OidResolver(IEnumerable<Lf2ObjectEntry> objects)
        {
            foreach (var o in objects)
                _byId[o.id] = o;
        }

        public bool TryResolve(int oid, out Lf2ObjectEntry entry) => _byId.TryGetValue(oid, out entry);

        public string GetRelativeFileOrEmpty(int oid)
        {
            return TryResolve(oid, out var e) ? e.file : "";
        }
    }
}
