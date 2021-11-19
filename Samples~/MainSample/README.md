This sample shows how to use the DualSense gamepad.

## Summary

All components are on the Scripts folder and implements the ```AbstractDualSenseBehaviour``` class. 
This class is used as a listener by the ```DualSenseMonitor``` component to set an instance of the ```DualSenseGamepadHID``` every time a DualSense gamepad is connected to the system.
The newer version of this example has an additionnal component which perform auto updates of the gamepad. This is necessary for the legacy rumbles which will stop after 5 seconds if no new outputReport is sent to the gamepad (which is what a gamepad update do). In this example this component is also used to set the trigger force feedback as the DualSenseTrigger component does not update the gamepad itself anymore.

The Scripts are:

1. DualSenseMonitor: notifies all the following components about a DualSense connection or disconnection and resets a DualSense instance when disabled.
2. DualSenseAutoUpdate: updates the gamepad regularly if it has not be done in a while and if needed (if using legacy rumbles or if the last update was not successful).
3. DualSenseButtonInput: displays all DualSense button inputs using SpriteRenderer instances.
4. DualSenseRumble: sets the DualSense rumble, aka motor speeds.
5. DualSenseTouchpadColor: sets the DualSense Touchpad light bar color.
6. DualSenseTrigger: sets the DualSense Triggers (but does not update the gamepad).
7. DualSensePauseHaptics: Pause or resumes the haptics on the gamepad (legacy rumbles and force feedbacks) 

Those components are used on the ```DualSense``` prefab. You can open the ```MainSample``` scene and check those features.