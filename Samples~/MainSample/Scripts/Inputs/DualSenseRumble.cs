namespace DualSenseSample.Inputs
{
    /// <summary>
    /// Component to set the DualSense rumble, aka motor speeds.
    /// <para>Set the <see cref="LeftRumble"/> and/or <see cref="RightRumble"/> Properties.</para>
    /// </summary>
    public class DualSenseRumble : AbstractDualSenseBehaviour
    {
        /// <summary>
        /// Speed of the low-frequency (left) motor. 
        /// Normalized [0..1] value with 1 indicating maximum speed 
        /// and 0 indicating the motor is turned off.
        /// </summary>
        public float LeftRumble { 
            get { return _leftRumble; }
            set
            {
                _leftRumble = value;
                UpdateMotorSpeeds();
            }
        }
        private float _leftRumble;

        /// <summary>
        /// Speed of the high-frequency (right) motor. 
        /// Normalized [0..1] value with 1 indicating maximum speed 
        /// and 0 indicating the motor is turned off.
        /// </summary>
        public float RightRumble
        {
            get { return _rightRumble; }
            set
            {
                _rightRumble = value;
                UpdateMotorSpeeds();
            }
        }
        private float _rightRumble;

        private void UpdateMotorSpeeds()
            =>
            DualSense?.SetMotorSpeeds(RightRumble, LeftRumble);
    }
}
