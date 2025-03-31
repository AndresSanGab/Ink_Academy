using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    private Rigidbody rb;

    void Start()
    {
    rb = GetComponent<Rigidbody>();

    if (rb != null)
    {
        Vector3 shootDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) // Arriba
            shootDirection = new Vector3(0, 1, 0);
        else if (Input.GetKey(KeyCode.S)) // Abajo
            shootDirection = new Vector3(0, -1, 0);
        else // Izquierda o derecha
            shootDirection = new Vector3(transform.localScale.x > 0 ? 1 : -1, 0, 0);

        rb.AddForce(shootDirection * speed, ForceMode.VelocityChange); // Aplicar fuerza en la dirección deseada

    }
    else
    {
        Debug.LogError("No se encontró Rigidbody en la bala. Asegúrate de que el prefab lo tenga.");
    }

    Destroy(gameObject, lifeTime);
    }


    void OnTriggerEnter(Collider collision)
    {
    if (collision.CompareTag("Enemy"))
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage();
        }
        Destroy(gameObject); // Destruye la bala después de causar daño
    }
    }

}
