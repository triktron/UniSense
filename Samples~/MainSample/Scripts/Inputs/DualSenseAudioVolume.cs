using UnityEngine;

namespace DualSenseSample.Inputs
{
    /// <summary>
    /// Component to set the DualSense Touchpad light bar color.
    /// <para>Set the <see cref="LightBarColor"/> Property to see the change.</para>
    /// </summary>
    public class DualSenseAudioVolume : AbstractDualSenseBehaviour
    {
        private float internalVolume;
        public float InternalVolume
        {
            get => internalVolume;
            set
            {
                internalVolume = value;
                DualSense?.SetAudioVolume(internalVolume, UniSense.DualSenseTargetAudioDevice.InternalSpeaker);
            }
        }

        private float externalVolume;
        public float ExternalVolume
        {
            get => externalVolume;
            set
            {
                externalVolume = value;
                DualSense?.SetAudioVolume(externalVolume, UniSense.DualSenseTargetAudioDevice.ExternalPluggedInDevice);
            }
        }
    }
}
