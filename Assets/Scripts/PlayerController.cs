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

    // Girar el sprite y el FirePoint
    if (moveInput.x > 0)
    {
        transform.localScale = new Vector3(1, 1, 1); // Mirando a la derecha
        firePoint.localRotation = Quaternion.Euler(0, 0, 0); // Apunta a la derecha
    }
    else if (moveInput.x < 0)
    {
        transform.localScale = new Vector3(-1, 1, 1); // Mirando a la izquierda
        firePoint.localRotation = Quaternion.Euler(0, 180, 0); // Apunta a la izquierda
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
    Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
