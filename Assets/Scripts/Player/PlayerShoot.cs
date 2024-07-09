using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;

    public bool canShoot;

    private Pool ammoPool;

    private void Start()
    {
        ammoPool = Pool.Instance;
    }

    void Update()
    {
        if (canShoot)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 tapPosition = Input.GetTouch(0).position;
                tapPosition.z = 10f;

                Vector3 target = Camera.main.ScreenToWorldPoint(tapPosition);

                Shoot(target);
            }
        }
    }


    void Shoot(Vector3 target)
    {
        var bullet = ammoPool.GetPooledObjects();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = firePoint.position;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 direction = (target - firePoint.position);
        rb.velocity = direction.normalized * bullet.GetComponent<Bullet>().shootForce;
    }
}