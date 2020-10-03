
using System;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 1;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        KillableFromAbove script = gameObject.AddComponent<KillableFromAbove>();
        script.RegisterOnDamageTaken(TakeDamage);
    }

    public void TakeDamage(int amount)
    {
        TakeDamage(amount, null);
    }

    public void TakeDamage(int amount, Action onDeathCallback)
    {
        health -= amount;
        if (health <= 0)
        {
            onDeathCallback?.Invoke();
            Destroy(gameObject, 0.0f);
        }
    }
}
