using UnityEngine;

namespace DMGeneralPrototype
{
    [System.Serializable]
    public struct Gear
    {
        public GameObject gear;

        public bool flipDirection;
    }

    [System.Serializable]
    public struct GearSettings
    {
        public Gear[] gears;

        public float gearSpeed;
    }

    [System.Serializable]
    public struct BeltSettings
    {
        public GameObject beltParentLower;
        public GameObject beltParentUpper;

        public float defaultSize;
    }

    [RequireComponent(typeof(Rigidbody))]
    public class Paddle : MonoBehaviour
    {
        public new Transform transform { get; private set; }

        public float speed;
        public float maxDistFromCenter = 5.0f;

        public GearSettings gearSettings;

        public BeltSettings beltSettings;

        public UpgradeMenuTest2.Upgrades upgrade;
        public UpgradeMenuTest2.Upgrades[] paddleLevels;

        public Transform prefabParent;
        public Transform gunParent;

        public Material material;

        public bool flipDir;

        public Vector3 paddlePhysicalSize;
        public Vector3 resetPosition;

        public Animator animator;

        public GameObject[] robotLevels;
        public Material[] robotMaterials;

        public SkinnedMeshRenderer robotMesh;

        bool paddleShrinked = false;

        int paddleSize = 1;

        private void Awake()
        {
            transform = GetComponent<Transform>();
        }

        private void Start()
        {
            resetPosition = transform.position;

            SpawnUpgrade();
            UpdatePrefabs();

            Debugger.Instance.DebugInfo("Paddle Width", paddlePhysicalSize.x.ToString());

            OnStart();
        }

        private void Update()
        {
            OnUpdate();

            beltSettings.beltParentLower.transform.localScale = new Vector3(1, 1, 1 + (transform.position.z / beltSettings.defaultSize));
            beltSettings.beltParentUpper.transform.localScale = new Vector3(1, 1, 1 - (transform.position.z / beltSettings.defaultSize));

            Debugger.Instance.DebugInfo("Paddle Pos", $"{transform.position}");
        }

        protected void Move(int dir)
        {
            Vector3 newPos = transform.position + Vector3.forward * dir * Time.deltaTime * speed;

            if (IsValidMove(newPos))
            {
                transform.position = newPos;

                for (int i = 0; i < gearSettings.gears.Length; i++)
                    gearSettings.gears[i].gear.transform.Rotate(0, gearSettings.gearSpeed * (gearSettings.gears[i].flipDirection ? -1 : 1) * dir, 0);
            }
        }

        private bool IsValidMove(Vector3 newPos)
        {
            return Mathf.Abs(newPos.z) < maxDistFromCenter;
        }

        public void ResetPaddle()
        {
            transform.position = resetPosition;
            UpdatePrefabs();
            SpawnUpgrade();
            RestorePaddle();
        }

        public void UpdatePrefabs()
        {
            for (int i = 0; i < prefabParent.childCount; i++)
                Destroy(prefabParent.GetChild(i).gameObject);

            GameObject newOBJ = Instantiate(paddleLevels[paddleShrinked ? 3 : paddleSize - 1].prefab, prefabParent);
            BoxCollider boxCollider = newOBJ.GetComponent<BoxCollider>();
            paddlePhysicalSize = boxCollider.size;
            PaddleColor color = newOBJ.GetComponent<PaddleColor>();
            color.material = material;
            color.UpdateMaterials();

            robotLevels[0].SetActive(paddleSize == 2);
            robotLevels[1].SetActive(paddleSize == 3);

            robotMesh.material = robotMaterials[paddleSize - 1];
        }

        public void UpdatePrefab(UpgradeMenuTest2.Upgrades upgrade)
        {
            this.upgrade = upgrade;
            UpdatePrefabs();
        }

        public void IncreasePaddleSize()
        {
            paddleSize += paddleSize > paddleLevels.Length - 2 ? 0 : 1;
            UpdatePrefabs();
        }

        public int GetPaddleSize()
        {
            return paddleSize;
        }

        public void ShrinkPaddle()
        {
            paddleShrinked = true;
            UpdatePrefabs();
        }

        public void RestorePaddle()
        {
            paddleShrinked = false;
            UpdatePrefabs();
        }

        public void SpawnUpgrade()
        {
            if (upgrade == null) 
            {
                gunParent.parent.GetComponent<LineScript>().linerenderer.enabled = false;
                return; 
            }

            if (upgrade.upgradeType == UpgradeMenuTest2.UpgradeType.ShrinkRay || upgrade.upgradeType == UpgradeMenuTest2.UpgradeType.StunGun)
            {
                Instantiate(upgrade.prefab, gunParent, false);
                gunParent.parent.GetComponent<LineScript>().linerenderer.enabled = true;
            }
            else
            {
                gunParent.parent.GetComponent<LineScript>().linerenderer.enabled = false;
            }
        }

        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
    }
}