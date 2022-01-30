using UnityEngine;
using UnityEngine.Events;

namespace DMGeneralPrototype
{
    public class Ball : MonoBehaviour
    {
        public float defaultSpeed;
        public float speedIncrease = 0.1f;
        public float speedCap = 20.0f;

        public AnimationCurve offsetCurve;

        public GameObject mesh;
        public GameObject aimMesh;

        public Material leftColor;
        public Material rightColor;
        public Material defaultColor;

        public UnityEvent paddleHitEvent;
        public UnityEvent wallHitEvent;

        public Vector2 startAngleRange = Vector2.zero;

        MeshRenderer meshRenderer;

        Vector3 velocity;

        Rigidbody rb;

        float circumference = 0;
        float speed;

        int lastHitPaddle = 2;

        bool started = false;

        void Start()
        {
            circumference = GetComponent<SphereCollider>().radius * Mathf.PI;

            rb = GetComponent<Rigidbody>();

            meshRenderer = GetComponentInChildren<MeshRenderer>();

            SetStartAngle();

            speed = defaultSpeed;
        }

        private void FixedUpdate()
        {
            rb.velocity = Vector3.zero;

            if (!started) return;

            rb.MovePosition(rb.position + velocity.normalized * Time.deltaTime * speed);

            Vector3 axis = Vector3.Cross(velocity.normalized, Vector3.down);
            float angle = velocity.magnitude * 360 / circumference;
            mesh.transform.Rotate(axis, angle * Time.deltaTime, Space.World);

            if (transform.position.x < -10.0f)
            {
                GameManager.instance.player2Scored.Invoke();
                GameManager.instance.player2Score++;
                GameManager.instance.CheckGameState();
            }
            else if (transform.position.x > 10.0f)
            {
                GameManager.instance.player1Scored.Invoke();
                GameManager.instance.player1Score++;
                GameManager.instance.CheckGameState();
            }

            speed = speed >= speedCap ? speedCap : speed + speedIncrease * Time.deltaTime;

            Debugger.Instance.DebugInfo("Ball Speed", $"{speed:00.00}");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                velocity = Vector3.Reflect(velocity, collision.GetContact(0).normal);
                rb.angularVelocity = Vector3.zero;

                wallHitEvent.Invoke();
            }
            else if (collision.collider.CompareTag("Paddle"))
            {
                if (collision.GetContact(0).normal == Vector3.forward || collision.GetContact(0).normal == Vector3.back)
                {
                    velocity = Vector3.Reflect(velocity, collision.GetContact(0).normal);
                    rb.angularVelocity = Vector3.zero;
                }
                else
                {
                    velocity.x = Vector3.Reflect(velocity, collision.GetContact(0).normal).x;

                    float offset = transform.position.z - collision.gameObject.transform.position.z;
                    velocity.z = offsetCurve.Evaluate(Mathf.Clamp01(Mathf.Abs(offset))) * (offset >= 0 ? 1 : -1);

                    rb.angularVelocity = Vector3.zero;
                }

                meshRenderer.material = transform.position.x >= 0 ? rightColor : leftColor;
                lastHitPaddle = transform.position.x >= 0 ? 2 : 1;

                paddleHitEvent.Invoke();
            }

            if (collision.collider.CompareTag("UpgradePoint"))
            {
                GameManager.instance.AddUpgradePoint(lastHitPaddle);
                Destroy(collision.gameObject);
            }
        }

        public void ResetBall()
        {
            transform.position = new Vector3(0, 3.25f, 0);
            SetStartAngle();
            meshRenderer.material = defaultColor;
            started = false;
            aimMesh.SetActive(true);
            speed = defaultSpeed;
        }

        public void StartGame()
        {
            started = true;
            aimMesh.SetActive(false);
        }

        public void SetStartAngle()
        {
            float angle = Random.Range(startAngleRange.x, startAngleRange.y);
            int invert = Random.Range(0, 2);
            Vector3 dir = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * (invert == 0 ? -1 : 1), 0, Mathf.Cos(angle * Mathf.Deg2Rad));

            velocity = dir;
            aimMesh.transform.LookAt(transform.position + dir);
        }

        const int stepCount = 25;

        private void OnDrawGizmos()
        {
            Vector3 minAngle = new Vector3(Mathf.Sin(startAngleRange.x * Mathf.Deg2Rad), 0, Mathf.Cos(startAngleRange.x * Mathf.Deg2Rad));
            Vector3 maxAngle = new Vector3(Mathf.Sin(startAngleRange.y * Mathf.Deg2Rad), 0, Mathf.Cos(startAngleRange.y * Mathf.Deg2Rad));

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + minAngle * 2);
            Gizmos.DrawLine(transform.position, transform.position + maxAngle * 2);

            Gizmos.DrawLine(transform.position, transform.position - minAngle * 2);
            Gizmos.DrawLine(transform.position, transform.position - maxAngle * 2);

            for (int i = 0; i < stepCount; i++)
            {
                float step = (startAngleRange.y - startAngleRange.x) / stepCount;

                Vector3 firstPoint = new Vector3(2 * Mathf.Sin((step * i + startAngleRange.x) * Mathf.Deg2Rad), 0, 2 * Mathf.Cos((step * i + startAngleRange.x) * Mathf.Deg2Rad));
                Vector3 secondPoint = new Vector3(2 * Mathf.Sin((step * (i + 1) + startAngleRange.x) * Mathf.Deg2Rad), 0, 2 * Mathf.Cos((step * (i + 1) + startAngleRange.x) * Mathf.Deg2Rad));

                Gizmos.DrawLine(transform.position + firstPoint, transform.position + secondPoint);
                Gizmos.DrawLine(transform.position - firstPoint, transform.position - secondPoint);
            }
        }
    }
}