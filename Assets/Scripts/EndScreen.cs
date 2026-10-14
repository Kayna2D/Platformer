using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public TMP_Text titleText, scoreText;

    void Start()
    {
        var gm = GameManager.Instance;
        titleText.text = gm.Won ? "VITÓRIA!" : "DERROTA";
        scoreText.text = $"Pontuação final: {gm.Score}";
    }
}
