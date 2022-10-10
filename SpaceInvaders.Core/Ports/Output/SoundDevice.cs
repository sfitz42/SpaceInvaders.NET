using System;

namespace SpaceInvaders.Core.Ports.Output
{
    public class SoundDevice
    {
        internal SoundOutput0 SoundOutput0 { get; }
        internal SoundOutput1 SoundOutput1 { get; }

        public EventHandler<SoundOutputChangeEventArgs> SoundChanged = null!;
        public EventHandler UFOEnd = null!;

        public SoundDevice()
        {
            SoundOutput0 = new(this);
            SoundOutput1 = new(this);
        }

        internal virtual void OnSoundOutputChange(SoundOutputChangeEventArgs e)
        {
            EventHandler<SoundOutputChangeEventArgs> hander = SoundChanged;

            hander?.Invoke(this, e);
        }

        internal virtual void OnUFOEnd(EventArgs e)
        {
            EventHandler hander = UFOEnd;

            hander?.Invoke(this, e);
        }
    }
}
