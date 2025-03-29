using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f; // Para que la bala no viva eternamente
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.right * speed; // Se moverá en la dirección que tenga al instanciarse
        Destroy(gameObject, lifeTime); // Destruir la bala después de un tiempo
    }

    void OnTriggerEnter(Collider collision) // Usamos Collider en 3D
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name); // Verifica si la colisión ocurre

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemigo golpeado!"); // Mensaje de prueba
            collision.GetComponent<Enemy>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
