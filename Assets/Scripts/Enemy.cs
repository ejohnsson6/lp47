
using System;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 1;
    private int health;

    [SerializeField] private float timeToSwapDirection = 2f;

    [SerializeField] private float speed = 2f;

    [SerializeField] private float startTime = 0f;
    
    private bool right;

    private float count;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        KillableFromAbove script = gameObject.AddComponent<KillableFromAbove>();
        script.RegisterOnDamageTaken(TakeDamage);

        right = true;
        count = startTime;
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

    private void Update()
    {
        count += Time.deltaTime;
        if (right)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * speed));
        }
        else
        {
            transform.Translate(Vector3.left * (Time.deltaTime * speed));
        }

        if (count > timeToSwapDirection)
        {
            right = !right;
            count = 0;
        }
    }
}
