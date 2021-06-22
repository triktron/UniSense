using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Dualsense_Controller_", menuName = "Dualsense Controller")]
public class DualsenseController : ScriptableObject
{
    public Color LightbarColor;

    public bool Led1;
    public bool Led2;
    public bool Led3;
    public bool Led4;
    public bool Led5;
}

#if UNITY_EDITOR

[CustomEditor(typeof(DualsenseController))]
[CanEditMultipleObjects]
public class DualsenseControllerEditor : Editor
{
    public SerializedProperty LightbarColor;

    public SerializedProperty Led1;
    public SerializedProperty Led2;
    public SerializedProperty Led3;
    public SerializedProperty Led4;
    public SerializedProperty Led5;

    void OnEnable()
    {
        LightbarColor = serializedObject.FindProperty("LightbarColor");

    Led1 = serializedObject.FindProperty("Led1");
    Led2 = serializedObject.FindProperty("Led2");
    Led3 = serializedObject.FindProperty("Led3");
    Led4 = serializedObject.FindProperty("Led4");
    Led5 = serializedObject.FindProperty("Led5");
}

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(LightbarColor);

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Player Leds:");
        Led1.boolValue = EditorGUILayout.Toggle(Led1.boolValue);
        Led2.boolValue = EditorGUILayout.Toggle(Led2.boolValue);
        Led3.boolValue = EditorGUILayout.Toggle(Led3.boolValue);
        Led4.boolValue = EditorGUILayout.Toggle(Led4.boolValue);
        Led5.boolValue = EditorGUILayout.Toggle(Led5.boolValue);

        EditorGUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();

        serializedObject.ApplyModifiedProperties();
    }
}

#endif