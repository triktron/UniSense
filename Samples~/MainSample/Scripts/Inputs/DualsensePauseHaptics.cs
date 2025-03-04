using DualSenseSample.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualsensePauseHaptics : AbstractDualSenseBehaviour
{
    public void PauseHaptics()
    {
        if (DualSense != null)
        {
            if (!DualSense.IsHapticPaused)
                DualSense.PauseHaptics();
            else
                DualSense.ResumeHaptics();
        }
    }
}
