using System.Collections;
using System.Collections.Generic;
using UniSense;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerLedSetter : MonoBehaviour
{
    public DualsenseControllerSet ControllerSet;

    private void Start()
    {
        var input = GetComponent<PlayerInput>();
        var controller = DualSenseGamepadHID.FindCurrent();

        if (controller == null || input == null) return;

        var playerNumber = input.playerIndex;

        var controllerSettings = ControllerSet.Controllers[playerNumber % ControllerSet.Controllers.Count];

        var state = new DualSenseGamepadState()
        {
            LightBarColor = controllerSettings.LightbarColor,
            PlayerLed = new PlayerLedState(controllerSettings.Led1, controllerSettings.Led2, controllerSettings.Led3, controllerSettings.Led4, controllerSettings.Led5, false)
        };

        controller.SetGamepadState(state);
    }
}
