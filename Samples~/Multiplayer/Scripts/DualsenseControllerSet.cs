using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dualsense_Controller_Set", menuName = "Dualsense Controller Set")]
public class DualsenseControllerSet : ScriptableObject
{
    public List<DualsenseController> Controllers;
}
