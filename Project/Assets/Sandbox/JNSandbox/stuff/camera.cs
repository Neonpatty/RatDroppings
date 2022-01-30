using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public float sens = 50f;
    public Transform tf;
    public Vector3 offset;
    public DMGeneralPrototype.PlayerPaddle paddle;

    float xRot;
    float yRot;
    float timer = 1.0f;

    bool singlePlayerOn = false;
    bool able = false;
    bool start = false;
    bool eggOn = false;
    bool center = false;

    Camera cam;


    void Start()
    {
        cam = GetComponent<Camera>();
        if (PlayerPrefs.GetString("mode") == "single")
        {
            singlePlayerOn = true;
        }
    }

    void Update()
    {
        if (singlePlayerOn)
        {
            if (eggOn == false)
            {
                if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.T))
                {
                    start = true;
                }
            }

            if (start)
            {
                transform.position = Vector3.Lerp(transform.position, tf.transform.position + offset, 0.05f);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(25, 90, 0), 0.075f);
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 90, 0.075f);
                if (Vector3.Distance(transform.position, tf.transform.position + offset) < 0.1f)
                {
                    start = false;
                    able = true;
                    yRot = 90;
                    xRot = 25;
                    paddle.upKey = KeyCode.A;
                    paddle.downKey = KeyCode.D;
                }
            }
            if (able)
            {
                float previousX = xRot;
                float previousY = yRot;


                float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sens;
                float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sens;
                xRot -= mouseY;
                yRot += mouseX;

                if (previousX != xRot || previousY != yRot)
                    timer = 1.0f;

                if (!center)
                {
                    xRot = Mathf.Clamp(xRot, -90f, 90f);
                    transform.localEulerAngles = new Vector3(xRot, yRot, 0f);

                    timer -= Time.deltaTime;
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(25, 90, 0), 0.075f);
                    xRot = transform.rotation.eulerAngles.x;
                    yRot = transform.rotation.eulerAngles.y;
                }

                center = timer <= 0;
            }
        }
    }
}
