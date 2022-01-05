using System;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace UniSense
{
    /// <summary>
    /// A struct containing nullables values describing a gamepad state. A null value means 
    /// "no changes since last state for this parameter"
    /// </summary>
    public struct DualSenseGamepadState
    {
        //TODO implement deep copy and deep Equals() comparison to detect and reproduce changes between states
        //TODO implement custom editor to display gamepadStates in the inspector

        public DualSenseTriggerState? RightTrigger;
        public DualSenseTriggerState? LeftTrigger;

        public bool? UseLegacyRumbles;
        public DualSenseMotorSpeed? Motor;

        public Color? LightBarColor;
        public DualSenseMicLedState? MicLed;
        public PlayerLedBrightness? PlayerLedBrightness;
        public PlayerLedState? PlayerLed;

        public float? internalVolume;
        public float? externalDeviceVolume;
    }

    public struct DualSenseMotorSpeed
    {
        public float LowFrequencyMotorSpeed;
        public float HighFrequencyMotorSpeed;

        public DualSenseMotorSpeed(float lowFrequencyMotorSpeed, float highFrequenceyMotorSpeed)
        {
            LowFrequencyMotorSpeed = lowFrequencyMotorSpeed;
            HighFrequencyMotorSpeed = highFrequenceyMotorSpeed;
        }

        public override bool Equals(object obj)
        {
            return obj is DualSenseMotorSpeed speed &&
                   LowFrequencyMotorSpeed == speed.LowFrequencyMotorSpeed &&
                   HighFrequencyMotorSpeed == speed.HighFrequencyMotorSpeed;
        }

        public override int GetHashCode()
        {
            int hashCode = -344924298;
            hashCode = hashCode * -1521134295 + LowFrequencyMotorSpeed.GetHashCode();
            hashCode = hashCode * -1521134295 + HighFrequencyMotorSpeed.GetHashCode();
            return hashCode;
        }
    }

    public enum DualSenseTargetAudioDevice
    {
        InternalSpeaker,
        ExternalPluggedInDevice,
        Both
    }

    public enum DualSenseMicLedState
    {
        Off,
        On,
        Pulsating,
    }

    public enum DualSenseTriggerEffectType : byte
    {
        NoResistance = 0,
        ResetResistance,
        ContinuousResistance,
        SectionResistance,
        EffectEx,
        //Calibrate,
    }

    [Serializable]
    //[StructLayout(LayoutKind.Explicit)] // having such an explicit layout is incompatible with unity inspector
    public struct DualSenseTriggerState
    {
        //[FieldOffset(0)]
        public DualSenseTriggerEffectType EffectType;

        //[FieldOffset(1)] 
        public DualSenseContinuousResistanceProperties Continuous;
        //[FieldOffset(1)] 
        public DualSenseSectionResistanceProperties Section;
        //[FieldOffset(1)] 
        public DualSenseEffectExProperties EffectEx;
    }

    [Serializable]
    public struct DualSenseContinuousResistanceProperties
    {
        public byte StartPosition;
        public byte Force;
    }

    [Serializable]
    public struct DualSenseSectionResistanceProperties
    {
        public byte StartPosition;
        public byte EndPosition;
        public byte Force;
    }

    [Serializable]
    public struct DualSenseEffectExProperties
    {
        public byte StartPosition;
        public bool KeepEffect;
        public byte BeginForce;
        public byte MiddleForce;
        public byte EndForce;
        public byte Frequency;
    }

    public enum PlayerLedBrightness
    {
        High,
        Medium,
        Low,
    }

    public struct PlayerLedState
    {
        private const byte LED1 = 0x10;
        private const byte LED2 = 0x08;
        private const byte LED3 = 0x04;
        private const byte LED4 = 0x02;
        private const byte LED5 = 0x01;
        private const byte LED_MASK = LED1 | LED2 | LED3 | LED4 | LED5;

        private const byte FADE = 0x40;

        public byte Value { get; private set; }

        public PlayerLedState(bool led1, bool led2, bool led3, bool led4, bool led5, bool fade = true)
        {
            Value = 0;
            if (led1) Value |= LED1;
            if (led2) Value |= LED2;
            if (led3) Value |= LED3;
            if (led4) Value |= LED4;
            if (led5) Value |= LED5;
            if (fade) Value |= FADE;
        }

        public PlayerLedState(byte led, bool fade = false)
        {
            Value = (byte)(led & LED_MASK);
            if (fade) Value |= FADE;
        }
    }
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DualSenseTriggerState))]
    public class UniqueObjectLogicProperty : PropertyDrawer
    {
        bool isFoldout = false;
        SerializedProperty effectType = null;
        SerializedProperty effectParameters;

        void Init(SerializedProperty property)
        {
            if (effectType is null)
                effectType = property.FindPropertyRelative("EffectType");
            
            switch ((DualSenseTriggerEffectType) effectType.enumValueIndex)
            {
                case DualSenseTriggerEffectType.ContinuousResistance:
                    effectParameters = property.FindPropertyRelative("Continuous");
                    break;

                case DualSenseTriggerEffectType.SectionResistance:
                    effectParameters = property.FindPropertyRelative("Section");
                    break;

                case DualSenseTriggerEffectType.EffectEx:
                    effectParameters = property.FindPropertyRelative("EffectEx");
                    break;

                case DualSenseTriggerEffectType.NoResistance:
                case DualSenseTriggerEffectType.ResetResistance:
                //case DualSenseTriggerEffectType.Calibrate:
                    effectParameters = null;
                    break;

                default:
                    Debug.LogError("Unimplemented Effect Type Value");
                    break;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);

            Rect subPos = new Rect(position);
            //EditorGUI.BeginProperty(position, label, property);
            subPos.height = EditorGUIUtility.singleLineHeight;
            isFoldout = EditorGUI.BeginFoldoutHeaderGroup(subPos, isFoldout, label);
            if (isFoldout)
            {
                EditorGUI.indentLevel++;
                
                subPos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                subPos.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(subPos, effectType, new GUIContent("Effect Type"), true);
                
                if (effectType.hasMultipleDifferentValues)
                {
                    subPos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.LabelField(subPos, "Cannot edit mutiple parameters when Effect Type is different");
                }
                else if (effectParameters != null)
                {
                    subPos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    subPos.height = EditorGUI.GetPropertyHeight(effectParameters);
                    EditorGUI.PropertyField(subPos, effectParameters, new GUIContent("Effect Parameters"), true);
                }

                EditorGUI.indentLevel--;
            }
            EditorGUI.EndFoldoutHeaderGroup();
            //EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Init(property);

            float total = EditorGUIUtility.singleLineHeight;
            if (isFoldout)
            {
                total += EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight; 
                if (effectType.hasMultipleDifferentValues)
                    total += EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;
                else if (effectParameters != null)
                    total += EditorGUIUtility.standardVerticalSpacing + EditorGUI.GetPropertyHeight(effectParameters);
            }
            return total;
        }
    }
#endif
}