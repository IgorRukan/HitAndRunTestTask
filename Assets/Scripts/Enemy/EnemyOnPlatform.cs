using UnityEngine;

public class EnemyOnPlatform : MonoBehaviour
{
    public HealthSystem[] enemies;
    private int numberOfEnemies;

    private GameManager gm;

    private void Start()
    {
        GetEnemies();
        numberOfEnemies = enemies.Length;

        foreach (var enemy in enemies)
        {
            enemy.Death += OnDeath;
        }

        gm = GameManager.Instance;
    }

    private void GetEnemies()
    {
        enemies = gameObject.GetComponentsInChildren<HealthSystem>();
    }

    public void ResetEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
            enemy.RagdollAlive();
        }
        numberOfEnemies = enemies.Length;
    }

    private void OnDeath(HealthSystem enemy)
    {
        numberOfEnemies--;
        
        if (numberOfEnemies == 0)
        {
            gm.Player.GetComponent<PlayerShoot>().canShoot = false;
            gm.Player.GetComponent<PlayerMove>().StartWalk();
        }
    }
}
