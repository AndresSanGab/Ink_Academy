using UnityEngine;
using UnityEngine.UI; // Necesario para UI
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 18f;
    private Rigidbody rb;
    private Vector3 moveDirection;
    public int maxHealth = 3;
    public GameObject[] hearts;
    public GameObject gameOverCanvas;
    public bool isGameOver = false;
    public bool hasKey = false;

    public Transform firePoint; // Punto de disparo
    public GameObject bulletPrefab; // Prefab de la bala
    public float bulletSpeed = 10f; // Velocidad de la bala

    public int currentHealth { get; private set; }

    // Variables para monedas y UI
    public int coins = 0; // Monedas del jugador (inicia en 0)
    public TextMeshProUGUI coinsText; // UI Text para mostrar las monedas
    public TextMeshProUGUI keyText; // Texto para mostrar la llave en el inventario

    private bool isFacingRight = true; // Dirección inicial
    public ShopManager shopManager;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHealthUI();
        gameOverCanvas.SetActive(false);
        coinsText.text = "Monedas: " + coins; // Mostrar las monedas al inicio
        keyText.text = "Llave: " + (hasKey ? "1" : "0"); // Mostrar la llave en el inventario
    }

    void Update()
    {
        // Obtén la entrada de movimiento
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Si el jugador se mueve, cambia el parámetro isWalking a true
        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("isWalking", true);  // Cambia el parámetro a true
        }
        else
        {
            animator.SetBool("isWalking", false);  // Cambia el parámetro a false
        }

        // Cambiar dirección del sprite si el jugador cambia de lado
        if (moveX > 0 && !isFacingRight)
            Flip();
        else if (moveX < 0 && isFacingRight)
            Flip();

        // Movimiento del jugador
        if (!isGameOver)
        {
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        }

        // Disparar solo cuando el jugador está quieto
        if (Input.GetKeyDown(KeyCode.Space) && moveDirection == Vector3.zero)
        {
            Shoot();
        }
    }


    void FixedUpdate()
    {
        if (isGameOver) return;

        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
        if (rbBullet != null)
        {
            rbBullet.isKinematic = false;
            rbBullet.useGravity = false;  // Evita que la bala caiga
        }

        // Dirección del disparo basada en la rotación del jugador
        float direction = isFacingRight ? 1f : -1f;

        // Aplicar velocidad
        rbBullet.linearVelocity = new Vector3(direction * bulletSpeed, 0, 0);

        // Ajustar rotación correcta de la bala
        bullet.transform.rotation = Quaternion.Euler(0, isFacingRight ? 0 : 180, 0);

        // Destruir la bala tras 2 segundos
        Destroy(bullet, 2f);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.rotation = Quaternion.Euler(0, isFacingRight ? -180 : 0, 0);

        // Girar firePoint también
        firePoint.localPosition = new Vector3(isFacingRight ? 1f : -1f, firePoint.localPosition.y, firePoint.localPosition.z);
    }

    public void TakeDamage(int damage)
    {
        if (isGameOver) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameOver();
        }

        UpdateHealthUI();
    }

    public void Heal()
    {
        if (isGameOver) return;

        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHealthUI();
        }
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHealth);
        }
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isGameOver = false;
        currentHealth = maxHealth;
        UpdateHealthUI();
        gameOverCanvas.SetActive(false);
        transform.position = new Vector3(0, 2.78f, 0);
    }

    public void AddCoins(int amount)
    {
        coins += amount;  // Incrementa las monedas
        coinsText.text = "Monedas: " + coins;  // Actualiza el texto de las monedas en la UI

        // Llama a UpdateCoinUI en el ShopManager para actualizar el texto en la tienda
        if (shopManager != null)
        {
            shopManager.UpdateCoinUI();  // Asegura que la tienda se actualice también
        }

        Debug.Log("Monedas actuales: " + coins);
    }


    public void AddKey()
    {
        hasKey = true; // El jugador ahora tiene la llave
        keyText.text = "Llave: 1"; // Muestra la llave en el inventario
        Debug.Log("Llave recogida");
    }

    public bool HasKey()
    {
        return hasKey; // Retorna si el jugador tiene la llave
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1; // Volver al tiempo normal por si el juego estaba pausado
        SceneManager.LoadScene("MainMenu");
    }

}
