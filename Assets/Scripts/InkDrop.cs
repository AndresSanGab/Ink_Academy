using UnityEngine;

public class InkDrop : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log("El jugador fue golpeado por tinta.");
            }
        }

        Destroy(gameObject); // Desaparece al tocar algo
    }
}
