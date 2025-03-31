using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel; // Referencia al panel de la tienda
    public int playerCoins = 10; // Monedas del jugador
    public int potionCost = 2;  // Precio de la poción
    public TextMeshProUGUI coinText; // Texto para mostrar monedas

    void Start()
    {
        shopPanel.SetActive(false); // Asegurar que la tienda esté oculta al inicio
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
    if (playerCoins >= potionCost)
    {
        playerCoins -= potionCost;
        UpdateCoinUI();

        // Simulamos agregar al inventario (por ahora solo mostramos un mensaje)
        Debug.Log("Poción comprada. Se añadirá al inventario cuando lo implementemos.");
        
        // Aquí guardamos la compra para usarla en el inventario después
        PlayerInventory.potionCount++;
    }
    }


    void UpdateCoinUI()
    {
        coinText.text = "Monedas: " + playerCoins;
        coinText.rectTransform.anchoredPosition = new Vector2(200, 10); // Ajusta estos valores a tu gusto
    }
}
