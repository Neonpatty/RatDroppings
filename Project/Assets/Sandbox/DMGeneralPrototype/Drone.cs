using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DMGeneralPrototype
{
    public class Drone : MonoBehaviour
    {
        public float speed = 1.0f;

        public GameObject[] upgradePrefabs;

        float lerpTimer = 0.0f;

        bool leaveArea = false;
        bool droppedItem = false;

        Vector3 startPos;
        Vector3 targetPos;
        Vector3 finishPos;

        Rigidbody upgradePointRigidBody;
        Animator animator;
        Transform upgradePointParent;

        void Start()
        {
            startPos = transform.position;

            upgradePointRigidBody = SpawnUpgradePoint(Vector3.zero + transform.position);

            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (lerpTimer >= 1.5f)
            {
                lerpTimer = 0.0f;
                leaveArea = true;
            }

            if (!droppedItem && lerpTimer >= 1.0f)
            {
                upgradePointRigidBody.useGravity = true;
                upgradePointRigidBody.transform.parent = upgradePointParent;
                animator.SetBool("ifClawOpen", true);
                droppedItem = true;
            }

            if (leaveArea)
            {
                transform.position = Vector3.Lerp(targetPos, finishPos, Mathf.SmoothStep(0, 1, lerpTimer));
                transform.LookAt(new Vector3(finishPos.x, transform.position.y, finishPos.z));
                animator.SetBool("ifClawOpen", false);
                if (lerpTimer >= 1.0f)
                    Destroy(gameObject);
            }
            else if (!leaveArea && lerpTimer <= 1.0f)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0, 1, lerpTimer));
                transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
            }

            lerpTimer += Time.deltaTime * speed;
        }

        public void SetTarget(Vector3 _targetPos, Vector3 _finishPos, Transform _upgradePointParent)
        {
            targetPos = _targetPos;
            finishPos = _finishPos;
            upgradePointParent = _upgradePointParent;
            
        }

        Rigidbody SpawnUpgradePoint(Vector3 spawnPos)
        {
            return Instantiate(ChoosePrefab(), spawnPos , Quaternion.identity, transform).GetComponent<Rigidbody>();
        }
        public GameObject ChoosePrefab()
        {
            int rand = Random.Range(0, 100);
            if(rand >= 20)
            {
                return upgradePrefabs[0];
            }
            else if (rand >= 5)
            {
                return upgradePrefabs[1];
            }
            else
            {
                return upgradePrefabs[2];
            }
        }
    }
}