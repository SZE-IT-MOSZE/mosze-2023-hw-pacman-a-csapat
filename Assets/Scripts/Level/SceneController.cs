using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A jelenetek kezeléséért felelõs osztály.
/// </summary>
public class SceneController : MonoBehaviour {
    /// <summary>
    /// A fõmenü jelenet sorszáma.
    /// </summary>
    public int mainMenuScene = 0;

    /// <summary>
    /// A város jelenet sorszáma.
    /// </summary>
    public int cityScene = 1;

    /// <summary>
    /// A következõ jelenet sorszáma.
    /// </summary>
    public int nextScene;

    /// <summary>
    /// A jobb felsõ UI wrapper RectTransform-je.
    /// </summary>
    public RectTransform topRightUiWrapper;

    /// <summary>
    /// A bal alsó UI wrapper RectTransform-je.
    /// </summary>
    public RectTransform bottomLeftUiWrapper;

    /// <summary>
    /// A játék vége panel RectTransform-je.
    /// </summary>
    public RectTransform gameOverPanel;

    /// <summary>
    /// Az átmeneti fedõ CanvasGroup.
    /// </summary>
    public CanvasGroup overlay;

    /// <summary>
    /// Az átmenet idõtartama másodpercben.
    /// </summary>
    public float transitionDuration = 1.3f;

    void Start() {
        // Start metódus jelenleg üres.
    }

    /// <summary>
    /// Aszinkron jelenet betöltés.
    /// </summary>
    /// <param name="scene">Betöltendõ jelenet sorszáma.</param>
    /// <param name="destroyGameManager">True, ha a GameManager objektumot törölni kell.</param>
    /// <returns>IEnumerator, amely a folyamatot vezérli.</returns>
    IEnumerator AsyncLoadScene(int scene, bool destroyGameManager) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        operation.allowSceneActivation = false;

        DOTween.KillAll();

        while (!operation.isDone) {
            if (operation.progress >= 0.9f) {
                if (destroyGameManager) {
                    GameObject gameManager = GameObject.Find("GameManager");

                    if (gameManager) {
                        Destroy(gameManager);
                    }
                }

                Time.timeScale = 1;
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    /// <summary>
    /// UI elemek animálása átmenetkor.
    /// </summary>
    /// <param name="isGameOver">True, ha a játék vége állapotban vagyunk.</param>
    /// <returns>IEnumerator, amely a folyamatot vezérli.</returns>
    public IEnumerator AnimateUIElements(bool isGameOver) {
        topRightUiWrapper.DOLocalMoveY(120f, transitionDuration).SetEase(Ease.OutQuad).SetRelative(true).SetUpdate(true);
        bottomLeftUiWrapper.DOLocalMoveY(-120f, transitionDuration).SetEase(Ease.OutQuad).SetRelative(true).SetUpdate(true);
        overlay.DOFade(.6f, transitionDuration).SetUpdate(true);

        yield return new WaitForSecondsRealtime(transitionDuration);

        if (isGameOver) {
            gameOverPanel.parent.gameObject.SetActive(true);
            gameOverPanel.parent.DOMoveY(Screen.height / 2, transitionDuration).SetUpdate(true);
        } else {
            StartCoroutine(AsyncLoadScene(nextScene, false));
        }

        yield return null;
    }

    /// <summary>
    /// Visszatérés a fõmenübe.
    /// </summary>
    public void ReturnToMainMenu() {
        StartCoroutine(AsyncLoadScene(mainMenuScene, true));
    }

    /// <summary>
    /// Játék újraindítása.
    /// </summary>
    public void RestartGame() {
        StartCoroutine(AsyncLoadScene(cityScene, true));
    }

    /// <summary>
    /// Kilépés a játékból.
    /// </summary>
    public void ExitGame() {
        Application.Quit();
    }

    void Update() {
        // Update metódus jelenleg üres.
    }
}
