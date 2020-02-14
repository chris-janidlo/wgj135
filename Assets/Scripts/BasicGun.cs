using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{
    public BasicBullet BulletPrefab;

    public float FiringForceMagnitude, HeatPerShot, TimePerShot;
    public string FireButton;

    public AudioSource FireSource;

    void Start ()
    {
        StartCoroutine(shootine());
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
                Player.Instance.Movement.Knockback(-firingForce);

                Player.Instance.Resources.Heat -= HeatPerShot;

                FireSource.Play();

                yield return new WaitForSeconds(TimePerShot);
            }
            else
            {
                yield return null;
            }
        }
    }
}
