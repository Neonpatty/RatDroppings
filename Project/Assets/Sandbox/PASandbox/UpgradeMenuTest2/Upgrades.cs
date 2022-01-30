using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpgradeMenuTest2
{
    public enum UpgradeType
    {
        ExtendedPaddle,
        StickyPaddle,
        PowerShot,
        StunGun,
        ShrinkRay,
    }

    [CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
    public class Upgrades : ScriptableObject
    {
        public new string name;
        public string description;

        public int cost;

        public UpgradeType upgradeType;

        public GameObject prefab;

        public int level = 1;
    }
}

