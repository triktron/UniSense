using UniSense;
using UnityEngine;
using UnityEngine.UI;

namespace DualSenseSample.Lights
{
    public class DualSenseTouchpadColor : AbstractDualSenseBehaviour
    {
        public Toggle Led1;
        public Toggle Led2;
        public Toggle Led3;
        public Toggle Led4;
        public Toggle Led5;

        public FlexibleColorPicker ColorPicker;
        public Image LedPanelSprite;

        Color _color;


        private void Start()
        {
            UpdatePlayerLedState();
        }

        private void Update()
        {
            if (_color != ColorPicker.color)
            {
                _color = ColorPicker.color;

                LedPanelSprite.color = _color;
                DualSense?.SetLightBarColor(_color);
            }
        }

        internal override void OnConnect(DualSenseGamepadHID dualSense)
        {
            base.OnConnect(dualSense);

            UpdatePlayerLedState();
        }

        public void SetBrightness(float state)
        {
            var brightness = PlayerLedBrightness.High;
            if (state == 1) brightness = PlayerLedBrightness.Medium;
            if (state == 0) brightness = PlayerLedBrightness.Low;

            DualSense?.SetPlayerLedsBrightness(brightness);
        }

        public void UpdatePlayerLedState()
        {
            var state = new PlayerLedState(Led1.isOn, Led2.isOn, Led3.isOn, Led4.isOn, Led5.isOn, true);

            DualSense?.SetPlayerLeds(state);
        }

        public void SetMicState(int state)
        {
            var ledState = DualSenseMicLedState.Off;
            if (state == 1) ledState = DualSenseMicLedState.On;
            if (state == 2) ledState = DualSenseMicLedState.Pulsating;

            DualSense?.SetMicLed(ledState);
        }

        private void OnDestroy()
        {
            DualSense?.Reset();
        }
    }
}
