using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TMP_Text scoreText, livesText, itemsText;

    void Update()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;
        scoreText.text = $"Pontos: {gm.Score}";
        livesText.text = $"Vidas: {gm.Lives}";
        itemsText.text = $"Dinheiro: {gm.Collected}/{gm.TotalItems}";
    }
}
