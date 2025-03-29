using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Arrastra el objeto del jugador aquí en el inspector
    public Vector3 offset = new Vector3(0, 10, -10); // Ajusta según la posición deseada

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset; // La cámara sigue al jugador
            transform.LookAt(player); // (Opcional) Hace que la cámara siempre mire al jugador
        }
    }
}
