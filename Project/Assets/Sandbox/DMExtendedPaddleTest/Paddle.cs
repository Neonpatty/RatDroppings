using System.Collections.Generic;
using UnityEngine;

namespace DMExtendedPaddleTest
{
    public class Paddle : MonoBehaviour
    {
        public Transform segmentParent;

        public Transform leftEnd;
        public Transform rightEnd;

        public GameObject segmentPrefab;

        public int segmentCount = 2;

        public float segmentOffset;
        public float segmentWidth;

        List<GameObject> segments = new List<GameObject>();

        Vector3 resetPosition;

        private void Start()
        {
            resetPosition = transform.position;

            ResetPaddle();
        }

        private void Update()
        {

        }

        public void ResetPaddle()
        {
            transform.position = resetPosition;
            UpdateSegments();
        }

        void UpdateSegments()
        {
            foreach (GameObject segment in segments)
                Destroy(segment);

            float totalWidth = segmentCount * segmentWidth;
            Debug.Log(totalWidth);

            for (int i = 0; i < segmentCount; i++)
            {
                GameObject newSegment = Instantiate(segmentPrefab, Vector3.right * ((i * segmentOffset) + (segmentWidth / 2.0f) - (totalWidth / 2.0f)), Quaternion.identity, segmentParent);

                segments.Add(newSegment);
            }

            rightEnd.position = Vector3.right * (((segmentCount - 1) * segmentOffset) + (segmentWidth / 2.0f) - (totalWidth / 2.0f));
            leftEnd.position = Vector3.right * ((segmentWidth / 2.0f) - (totalWidth / 2.0f));
        }
    }
}