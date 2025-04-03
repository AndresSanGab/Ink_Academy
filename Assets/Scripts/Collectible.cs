using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Coin, Key }
    public CollectibleType collectibleType; // Tipo de objeto coleccionable (Moneda o Llave)

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Si es el jugador quien entra en contacto
        {
            PlayerController player = other.GetComponent<PlayerController>(); // Obtener el PlayerController del jugador
            if (player == null) return; // Si no se encuentra el PlayerController, salimos.

            switch (collectibleType)
            {
                case CollectibleType.Coin:
                    player.AddCoins(1);  // Incrementa las monedas del jugador
                    Debug.Log("Moneda recogida");
                    break;
                case CollectibleType.Key:
                    if (!player.HasKey()) // Si no tiene la llave, recogerla
                    {
                        player.AddKey();
                        Debug.Log("Llave recogida");
                    }
                    break;
            }

            // Destruye el objeto después de ser recogido
            Destroy(gameObject);
        }
    }
}
