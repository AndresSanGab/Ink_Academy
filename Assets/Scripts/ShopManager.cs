using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel; // Referencia al panel de la tienda
    public int potionCost = 2;  // Precio de la poción
    public TextMeshProUGUI coinText; // Texto para mostrar monedas

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
    if (PlayerInventory.coinCount >= potionCost)
    {
        PlayerInventory.coinCount -= potionCost;
        UpdateCoinUI();

        // Simulamos agregar al inventario (por ahora solo mostramos un mensaje)
        Debug.Log("Poción comprada. Se añadirá al inventario cuando lo implementemos.");
        
        // Aquí guardamos la compra para usarla en el inventario después
        PlayerInventory.potionCount++;
    }
    }


    void UpdateCoinUI()
    {
        coinText.text = "Monedas: " + PlayerInventory.coinCount;
        coinText.rectTransform.localPosition = new Vector3(200, 10, 0);
    }
}
