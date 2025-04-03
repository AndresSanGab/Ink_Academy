using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel; // Referencia al panel de la tienda
    public int potionCost = 1;  // Precio de la poción
    public TextMeshProUGUI coinText; // Texto para mostrar monedas
    public PlayerController player; // Referencia al PlayerController para acceder a las monedas

    void Start()
    {
        shopPanel.SetActive(false);
        coinText.enableWordWrapping = false; // Evita saltos de línea
        coinText.overflowMode = TextOverflowModes.Overflow; // Permite que el texto continúe sin cortar
        coinText.rectTransform.localPosition = new Vector3(200, 10, 0);
        UpdateCoinUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleShop();
        }
    }

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }

    void BuyPotion()
    {
        if (player.coins >= potionCost)  // Usamos player.coins en lugar de PlayerInventory.coinCount
        {
            player.coins -= potionCost;  // Actualiza las monedas del jugador
            UpdateCoinUI();

            // Simulamos agregar al inventario
            Debug.Log("Poción comprada. Se añadirá al inventario cuando lo implementemos.");

            // Aquí guardamos la compra para usarla en el inventario después
            PlayerInventory.potionCount++;
        }
    }

    public void UpdateCoinUI()
    {
        coinText.text = "Monedas: " + player.coins;  // Usa player.coins en lugar de PlayerInventory.coinCount
        coinText.rectTransform.localPosition = new Vector3(200, 10, 0);
    }
}
