using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 2;
    public GameObject coinPrefab;
    public GameObject keyPrefab;
    public float moveSpeed = 2f;
    
    private Vector2 moveDirection;
    private bool keyDropped = false;

    void Start()
    {
        ChangeDirection();
        InvokeRepeating(nameof(ChangeDirection), 2f, 2f);
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void ChangeDirection()
    {
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    void DropItem()
    {
        if (!keyDropped && Random.value < 0.2f) // 20% de probabilidad de soltar llave
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
            keyDropped = true;
        }
        else
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
