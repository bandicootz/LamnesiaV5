using System;
using UnityEngine;

public static class Models
{
    [Serializable]
    public class WeaponSettingsModel
    {
        [Header("Sway")]
        public float SwayAmount;
        public bool SwayYInverted;
        public bool SwayXInverted;
        public float SwaySmoothing;
    }
}