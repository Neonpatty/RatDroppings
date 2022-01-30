using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UpgradeMenuTest2
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStat")]
    public class PlayerStats : ScriptableObject
    {

        //VARS
        public string playerName;
        public int playerScore;

    }
}