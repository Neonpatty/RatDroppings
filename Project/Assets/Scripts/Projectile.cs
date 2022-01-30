using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public Vector3 target;

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        if (Vector3.Distance(transform.position, target) < 1.0f)
            Destroy(gameObject);
    }
}
