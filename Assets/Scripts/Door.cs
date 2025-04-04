using UnityEngine;

public class Door : MonoBehaviour
{
    public PlayerController player;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.HasKey())
        {
            gameObject.SetActive(false);
            Debug.Log("Puerta abierta con llave.");
        }
    }

}
