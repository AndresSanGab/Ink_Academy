using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 2;
    public GameObject coinPrefab;
    public GameObject keyPrefab;
    public float moveSpeed = 2f;

    private Vector3 moveDirection;
    private bool keyDropped = false;

    void Start()
    {
        ChangeDirection();
        InvokeRepeating(nameof(ChangeDirection), 2f, 2f);
    }

    void Update()
    {
        // Aquí no deberíamos mover al enemigo, ya que lo queremos fijo por ahora
        // transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void ChangeDirection()
    {
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(1); // Resta vida al jugador
        }
    }

    public void TakeDamage()
    {
        health--;
        Debug.Log("Enemigo recibió daño, vida restante: " + health);

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
