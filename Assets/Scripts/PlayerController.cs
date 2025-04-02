using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    private Rigidbody rb; // Referencia al Rigidbody
    private Vector3 moveDirection; // Dirección del movimiento
    public int maxHealth = 3; // Vida máxima
    public GameObject[] hearts; // Corazones para mostrar en la UI
    public GameObject gameOverCanvas; // Pantalla de Game Over
    public bool isGameOver = false; // Estado de Game Over

    // Hacer la propiedad 'currentHealth' pública solo para lectura
    public int currentHealth { get; private set; } // No es posible modificarla directamente desde fuera

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody del jugador
        currentHealth = maxHealth; // Inicializa la vida del jugador
        UpdateHealthUI(); // Actualiza la UI de los corazones
        gameOverCanvas.SetActive(false); // Asegura que la pantalla de Game Over esté oculta al inicio
    }

    void Update()
    {
        if (isGameOver) return; // Si el juego ha terminado, no mueve al jugador

        // Obtén las entradas del teclado
        float moveX = Input.GetAxisRaw("Horizontal"); // Movimiento horizontal
        float moveZ = Input.GetAxisRaw("Vertical"); // Movimiento en el eje Z (adelante y atrás)
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized; // Calcula la dirección de movimiento
    }

    void FixedUpdate()
    {
        if (isGameOver) return; // Si el juego ha terminado, no mueve al jugador

        // Mueve al jugador utilizando el Rigidbody
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        if (isGameOver) return; // No hace nada si el juego ha terminado

        currentHealth -= damage; // Reducir vida por el daño

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameOver(); // Llama al Game Over cuando el jugador se queda sin vida
        }

        UpdateHealthUI(); // Actualiza la UI
    }

    public void Heal()
    {
        if (isGameOver) return; // No permite curarse si el juego terminó

        if (currentHealth < maxHealth)
        {
            currentHealth++; // Aumenta la vida
            UpdateHealthUI(); // Actualiza la UI
        }
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].SetActive(true); // Activa el corazón si tiene vida
            }
            else
            {
                hearts[i].SetActive(false); // Desactiva el corazón si no tiene vida
            }
        }
    }

    void GameOver()
    {
        isGameOver = true; // Marca el estado de Game Over
        gameOverCanvas.SetActive(true); // Muestra la pantalla de Game Over
        Time.timeScale = 0; // Detiene el tiempo en el juego (opcional)
    }

    public void RestartGame()
    {
        // Aquí puedes reiniciar el juego
        Time.timeScale = 1; // Reactiva el tiempo
        isGameOver = false;
        currentHealth = maxHealth;
        UpdateHealthUI();
        gameOverCanvas.SetActive(false);

        // Opcionalmente, también puedes reiniciar la posición del jugador
        transform.position = Vector3.zero; // O cualquier otra posición inicial
    }
}
