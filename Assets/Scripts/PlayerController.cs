using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody rb; // Cambiamos a Rigidbody en lugar de Rigidbody2D
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Usamos Rigidbody en lugar de Rigidbody2D
    }

    void Update()
    {
        // Movimiento
        moveInput.x = Input.GetAxis("Horizontal"); // Movimiento en el eje X
        moveInput.z = Input.GetAxis("Vertical");   // Movimiento en el eje Z
        moveInput.y = 0; // Aseguramos que no haya movimiento en el eje Y

        // Disparo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed; // Aplicamos movimiento al Rigidbody
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }
}
