using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 2;
    public GameObject coinPrefab;
    public GameObject keyPrefab;
    public float moveSpeed = 10f;

    private Vector3 moveDirection;
    private bool keyDropped = false;

    void Start()
    {
        ChangeDirection();  // Asignar una dirección inicial aleatoria
        InvokeRepeating(nameof(ChangeDirection), 2f, 2f);  // Cambiar la dirección cada 2 segundos
    }

    void Update()
    {
        // Mover al enemigo constantemente en la dirección asignada
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void ChangeDirection()
    {
        // Cambiar la dirección de forma aleatoria
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el enemigo toca algo que no sea el jugador, cambia de dirección
        if (!other.CompareTag("Player"))
        {
            ChangeDirection();  // Cambiar la dirección si toca una pared u objeto
        }

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
        PlayerController player = FindObjectOfType<PlayerController>();

        if (!player.hasKey)
        {
            // Si el jugador no tiene la llave, soltar moneda y llave separadas
            Vector3 coinDropPos = transform.position + new Vector3(-1.5f, 0, 0); // Moneda a la izquierda
            Vector3 keyDropPos = transform.position + new Vector3(1.5f, 0, 0);   // Llave a la derecha

            Instantiate(coinPrefab, coinDropPos, Quaternion.identity);
            Instantiate(keyPrefab, keyDropPos, Quaternion.identity);
        }
        else
        {
            // Si ya tiene la llave, solo soltar la moneda justo en el centro
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
