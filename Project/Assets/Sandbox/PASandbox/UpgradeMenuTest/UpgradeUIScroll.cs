using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpgradeMenuTest
{
    public class UpgradeUIScroll : MonoBehaviour
    {
        public int index = 0;
        public int upgradeCount;

        public float smoothSpeed = 0.15f;
        public float upgradeOffset = 0.2f;
        
        public Transform upgradeContainer;

        public KeyCode left;
        public KeyCode right;
        
        Vector3 startPosition;
        Vector3 targetPosition;

        private void Start()
        {
            startPosition = upgradeContainer.localPosition;    
        }

        void Update()
        {
            if (Input.GetKeyDown(left) && index > 0)
            {
                index--;
            }
            else if (Input.GetKeyDown(right) && index < upgradeCount-1)
            {
                index++;
            }

            targetPosition = startPosition + (-upgradeContainer.right * index * upgradeOffset);

            upgradeContainer.localPosition = Vector3.Lerp(upgradeContainer.localPosition, targetPosition, smoothSpeed);
        }
    }
}
