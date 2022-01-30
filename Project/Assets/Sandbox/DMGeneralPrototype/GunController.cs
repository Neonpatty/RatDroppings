using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public new Transform transform { get; private set; }

    public Transform target;

    public DMGeneralPrototype.Player player;

    public DMGeneralPrototype.Paddle otherPaddle;

    [HideInInspector]
    public KeyCode activationKey;

    public Image cooldownImage;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void Start()
    {
        activationKey = KeybindManager.Instance.GetKeybind(player == DMGeneralPrototype.Player.P1 ? "P1ActionKey" : "P2ActionKey");
    }

    private void Update()
    {
        Vector3 offset = transform.InverseTransformPoint(target.position);

        float angle = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;

        transform.Rotate(0, angle, 0);
    }
}
