using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    [SerializeField]
    private float currentHealth;
    private float currentHealthPercentage;
    
    private GameObject player;

    public List<Rigidbody> rbElements;
    public bool dead;
    [Header("For Ragdoll")]
    public float forceMagnitude;
    private Vector3 hitPoint;
    private Collider hitCollider;

    public Canvas canvas;
    [SerializeField]
    private Slider enemyHealthBar;
    
    public event Action<HealthSystem> Death;

    private void Start()
    {
        player = GameManager.Instance.Player;
        
        enemyHealthBar = canvas.GetComponentInChildren<Slider>();

        Init();
    }

    private void Init()
    {
        currentHealth = maxHealth;
        ChangeHealthBar();
    }

    public void FullRestore()
    {
        currentHealth = maxHealth;
        ChangeHealthBar();
    }
    public void GetDamage(float value,Vector3 hitPoint, Collider hitCollider)
    {
        currentHealth -= value;

        this.hitPoint = hitPoint;
        this.hitCollider = hitCollider;
        ChangeHealthBar();
        CheckIsDead();
    }

    private void ChangeHealthBar()
    {
        currentHealthPercentage = currentHealth / maxHealth;
        enemyHealthBar.value = currentHealthPercentage;
    }

    private void CheckIsDead()
    {
        if (!(currentHealth <= 0)) return;
        RagdollDeath();
        Death?.Invoke(this);
        currentHealth = 0;
    }

    private void RagdollDeath()
    {
        dead = true;
        canvas.enabled = false;
        
        GetComponent<Animator>().enabled = false;
        foreach (var element in rbElements)
        {
            element.isKinematic = false;
            hitPoint = new Vector3(0,0,0);
            if (hitCollider.transform.IsChildOf(element.transform))
            {
                Vector3 directionToHitPoint = (element.position - hitPoint).normalized;
                element.AddForce(directionToHitPoint * forceMagnitude, ForceMode.Impulse);
            }
        }
    }

    public void RagdollAlive()
    {
        dead = false;
        canvas.enabled = true;
        
        GetComponent<Animator>().enabled = true;
        foreach (var element in rbElements)
        {
            element.isKinematic = true;
        }
        FullRestore();
    }
        

    private void Update()
    {
        if (!dead)
        {
            transform.LookAt(player.transform);
            if (Camera.main != null) canvas.transform.LookAt(Camera.main.transform);
        }
    }
}
