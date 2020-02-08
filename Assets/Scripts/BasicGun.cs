using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{
    public BasicBullet BulletPrefab;

    public float FiringForceMagnitude, HeatPerShot, TimePerShot;
    public string FireButton, SwitchDirectionButton;

    public Rigidbody PlayerRigidbody;

    void Start ()
    {
        StartCoroutine(shootine());
    }

    void Update ()
    {
        bool shouldSwitch = Input.GetButton(SwitchDirectionButton);

        transform.localEulerAngles = new Vector3(0, shouldSwitch ? 180 : 0, 0);
    }

    IEnumerator shootine ()
    {
        while (true)
        {
            if (Input.GetButton(FireButton))
            {
                Vector3 firingForce = transform.forward * FiringForceMagnitude;

                BasicBullet bullet = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(firingForce));

                bullet.Rigidbody.AddForce(firingForce, ForceMode.VelocityChange);
                PlayerRigidbody.AddForce(-firingForce, ForceMode.VelocityChange);

                // subtract heat

                yield return new WaitForSeconds(TimePerShot);
            }
            else
            {
                yield return null;
            }
        }
    }
}
