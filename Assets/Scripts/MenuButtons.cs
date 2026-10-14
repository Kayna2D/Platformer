using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void Play() => GameManager.Instance.NewGame();
    public void BackToMenu() => GameManager.Instance.GoToMenu();
    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
