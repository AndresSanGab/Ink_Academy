using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
    moveInput.x = Input.GetAxis("Horizontal"); 
    moveInput.z = Input.GetAxis("Vertical"); 
    moveInput.y = 0; 

    // Movimiento
    if (moveInput.x > 0) // Si se mueve a la derecha
    {
        transform.rotation = Quaternion.Euler(0, 180, 0); // Mantiene la rotación original
    }
    else if (moveInput.x < 0) // Si se mueve a la izquierda
    {
        transform.rotation = Quaternion.Euler(0, 0, 0); // Rota 180° en Y para mirar a la izquierda
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
        Shoot();
    }
    }


    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed; // Aplicamos movimiento en 3D
    }

    void Shoot()
    {
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Rigidbody rb = bullet.GetComponent<Rigidbody>();

    if (rb != null)
    {
        Vector3 shootDirection = new Vector3(transform.localScale.x > 0 ? 1 : -1, 0, 0); // Dispara en función de la escala en X
        rb.linearVelocity = shootDirection * 10f; // Aplica velocidad con Rigidbody normal
    }
    else
    {
        Debug.LogError("La bala instanciada no tiene un Rigidbody.");
    }
    }
}
