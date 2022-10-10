using System;

namespace SpaceInvaders.Core.Ports.Output
{
    public class SoundOutputChangeEventArgs : EventArgs
    {
        public SoundType Sound { get; set; }

        public SoundOutputChangeEventArgs(SoundType sound)
        {
            Sound = sound;
        }
    }
}
