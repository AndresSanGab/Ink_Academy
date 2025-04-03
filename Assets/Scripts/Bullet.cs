using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisiona tiene el tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Llama al método TakeDamage del enemigo y pasa el daño
            other.GetComponent<Enemy>().TakeDamage(1); // Aplica el daño al enemigo
            Destroy(gameObject); // Destruye la bala después del impacto
        }

        // Si colisiona con el jugador, no hacer nada, ya que no debe impactar con él
        // (Este código es opcional, por si deseas realizar alguna acción si la bala toca al jugador)
        else if (other.CompareTag("Player"))
        {
            // No hacer nada si toca al jugador
            Debug.Log("La bala tocó al jugador pero no debe causar daño");
        }
    }
}
