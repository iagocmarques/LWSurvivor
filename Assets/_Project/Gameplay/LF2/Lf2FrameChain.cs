namespace Project.Gameplay.LF2
{
    /// <summary>
    /// Navigates through LF2 frames by following the 'next' pointer.
    /// Handles wait countdown and auto-advance.
    /// </summary>
    public sealed class Lf2FrameChain
    {
        private Lf2CharacterData _character;
        private Lf2FrameData _current;
        private int _waitRemaining;

        public Lf2FrameData Current => _current;
        public int WaitRemaining => _waitRemaining;
        public bool IsFinished => _current != null && (_current.Next == 999 || _current.Next < 0) && _waitRemaining <= 0;

        public Lf2FrameChain(Lf2CharacterData character)
        {
            _character = character;
        }

        /// <summary>
        /// Set the current frame by id. Returns true if frame exists.
        /// </summary>
        public bool SetFrame(int frameId)
        {
            if (_character == null || !_character.Frames.TryGetValue(frameId, out var frame))
            {
                _current = null;
                return false;
            }

            _current = frame;
            _waitRemaining = frame.Wait;
            return true;
        }

        /// <summary>
        /// Force advance to the next frame (ignoring wait).
        /// </summary>
        public bool Advance()
        {
            if (_current == null) return false;
            if (_current.Next == 999 || _current.Next < 0 || _current.Next == _current.Id) { _waitRemaining = 0; return false; }
            return SetFrame(_current.Next);
        }

        /// <summary>
        /// Tick the wait counter. Returns true if frame auto-advanced.
        /// </summary>
        public bool Tick()
        {
            if (_current == null) return false;
            if (_waitRemaining > 0) { _waitRemaining--; return false; }
            return Advance();
        }
    }
}
