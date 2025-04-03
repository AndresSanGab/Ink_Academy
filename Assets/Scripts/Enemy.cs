using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 2;
    public GameObject coinPrefab;
    public GameObject keyPrefab;
    public float moveSpeed = 2f;

    private Vector3 moveDirection;
    private bool keyDropped = false;
    private static bool firstEnemyDead = false; // Variable estática para rastrear si el primer enemigo muere

    void Start()
    {
        ChangeDirection();
        InvokeRepeating(nameof(ChangeDirection), 2f, 2f);
    }

    void Update()
    {
        // No necesitamos mover al enemigo ahora
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemigo recibió daño, vida restante: " + health);

        if (health <= 0)
        {
            DropItem();
            Destroy(gameObject); // El enemigo muere
        }
    }

    void DropItem()
    {
        if (!firstEnemyDead)  // Si es el primer enemigo que muere
        {
            // Separa un poco la posición de la llave y la moneda
            Vector3 dropOffset = new Vector3(0.5f, 0, 0); // Ajusta esta cantidad para mayor o menor separación

            // Instanciamos la llave y la moneda con una pequeña separación
            Instantiate(keyPrefab, transform.position + dropOffset, Quaternion.identity);
            Instantiate(coinPrefab, transform.position - dropOffset, Quaternion.identity);
            
            firstEnemyDead = true; // Ya no será el primer enemigo
        }
        else
        {
            // Si el jugador tiene llave, solo suelta moneda
            if (!FindObjectOfType<PlayerController>().hasKey) 
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
        }
    }

}
