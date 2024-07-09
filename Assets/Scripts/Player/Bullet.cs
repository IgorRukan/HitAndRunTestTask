using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    public float shootForce;

    public float destroyHeight;

    void FixedUpdate()
    {
        CheckBulletValidity();
    }

    void CheckBulletValidity()
    {
        if (transform.position.y < destroyHeight)
        {
            ReturnObjectToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<HealthSystem>() && !other.GetComponentInParent<HealthSystem>().dead)
        {
            Vector3 hitPoint = other.ClosestPointOnBounds(transform.position);

            other.GetComponentInParent<HealthSystem>().GetDamage(damage,hitPoint,other);
        }

        ReturnObjectToPool();
    }

    private void ReturnObjectToPool()
    {
        gameObject.SetActive(false);
    }
}