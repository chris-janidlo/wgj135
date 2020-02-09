using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{
    public BasicBullet BulletPrefab;

    public float FiringForceMagnitude, HeatPerShot, TimePerShot;
    public string FireButton, SwitchDirectionButton;

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
                Player.Instance.Movement.Rigidbody.AddForce(-firingForce, ForceMode.VelocityChange);

                Player.Instance.Resources.Heat -= HeatPerShot;

                yield return new WaitForSeconds(TimePerShot);
            }
            else
            {
                yield return null;
            }
        }
    }
}
