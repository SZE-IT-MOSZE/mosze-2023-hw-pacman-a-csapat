using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public string newGameScene;

    void Start () {
        
    }

    public void newGame()
    {
        // SceneManager.LoadScene(newGameScene);
        Debug.Log("Betöltés animáció, majd első pálya scene load...");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
