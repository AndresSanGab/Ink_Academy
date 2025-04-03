using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public TextMeshProUGUI potionCountText; // Texto para mostrar cantidad de pociones
    public TextMeshProUGUI coinText; // Texto para mostrar cantidad de monedas
    public TextMeshProUGUI keyText; // Texto para mostrar la llave en el inventario
    public GameObject inventoryPanel; // Panel del inventario
    public Button usePotionButton; // Botón para usar poción
    public PlayerController player; // Referencia al jugador

    void Start()
    {
        inventoryPanel.SetActive(false); // Inventario oculto al inicio
        usePotionButton.onClick.AddListener(UsePotion); // Asigna el botón para usar pociones
        UpdateInventoryUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        potionCountText.text = "Pociones: " + PlayerInventory.potionCount;
        coinText.text = "Monedas: " + player.coins; // Actualiza las monedas del jugador
        keyText.text = "Llave: " + (player.HasKey() ? "1" : "0"); // Muestra la llave si el jugador la tiene

        // Desactiva el botón si no hay pociones o si el jugador ya tiene vida máxima
        usePotionButton.interactable = PlayerInventory.potionCount > 0 && player.currentHealth < player.maxHealth;
    }

    public void UsePotion()
    {
        if (PlayerInventory.potionCount > 0 && player.currentHealth < player.maxHealth)
        {
            PlayerInventory.potionCount--; // Reduce una poción
            player.Heal(); // Llama al método de curación en el jugador
            UpdateInventoryUI(); // Actualiza el inventario
        }
    }
}
