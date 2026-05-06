using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 100f;
    public int damage = 25;
    public AudioSource gunSound;
    public Camera fpsCam;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (gunSound != null)
            gunSound.Play();

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            Target target = hit.transform.GetComponentInParent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
            else
            {
                Debug.Log("No Target found on hit object");
            }
        }
    }
}