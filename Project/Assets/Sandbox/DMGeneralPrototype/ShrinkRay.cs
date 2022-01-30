using UnityEngine;
using UnityEngine.UI;

public class ShrinkRay : MonoBehaviour
{
    public GameObject projectile;

    DMGeneralPrototype.Paddle otherPaddle;
    Transform target;

    float cooldownTimer = 10.0f;

    bool restored = false;
    bool canShoot = false;

    KeyCode activate;

    Image cooldown;

    LineScript line;

    private void Start()
    {
        GunController gun = transform.parent.parent.GetComponent<GunController>();
        target = gun.target;
        otherPaddle = gun.otherPaddle;
        activate = gun.activationKey;
        cooldown = gun.cooldownImage;

        line = gun.GetComponent<LineScript>();
    }

    void Update()
    {
        if (target.position.z < otherPaddle.transform.position.z + otherPaddle.paddlePhysicalSize.x / 2 && target.position.z > otherPaddle.transform.position.z - otherPaddle.paddlePhysicalSize.x / 2 && canShoot)
        {
            line.linerenderer.startColor = line.glowColor;
        }
        else
        {
            line.linerenderer.startColor = line.playerColor;
        }

        if (Input.GetKeyDown(activate) && canShoot)
        {
            cooldownTimer = 0.0f;
            Shoot();

            if (target.position.z < otherPaddle.transform.position.z + otherPaddle.paddlePhysicalSize.x / 2 && target.position.z > otherPaddle.transform.position.z - otherPaddle.paddlePhysicalSize.x / 2)
                otherPaddle.ShrinkPaddle();

            restored = false;
            canShoot = false;
        }

        if (!restored && cooldownTimer >= 5.0f)
        {
            otherPaddle.RestorePaddle();
            restored = true;
        }

        if (cooldownTimer >= 10.0f)
            canShoot = true;

        cooldownTimer += Time.deltaTime;

        cooldown.fillAmount = cooldownTimer / 10.0f;
    }

    void Shoot()
    {
        GameObject newObj = Instantiate(projectile, transform.position, Quaternion.identity);
        newObj.transform.LookAt(target);
        newObj.GetComponent<Projectile>().target = target.position;
    }
}
