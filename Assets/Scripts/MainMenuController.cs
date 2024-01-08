using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using EasyProgressBar;

public class MainMenuController : MonoBehaviour {

    public int nextScene;

    public Button playButton;
    public Button exitButton;
    public RectTransform logo;
    public ProgressBar progressBar;

    public float transitionDuration = 1.6f;

    void Start () {

    }

    IEnumerator LoadScene() {

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;
        float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

        while (progressValue < .8f)
        {
            progressValue += .01f;
            progressBar._fillAmount = progressValue;

            yield return new WaitForSeconds(.01f);
        }

        while (!operation.isDone) {

            progressBar._fillAmount = progressValue - .8f;

            if (operation.progress >= 0.9f) {
                progressBar._fillAmount = 1f;

                yield return new WaitForSeconds(.4f);

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    IEnumerator MoveOutUIElements() {
        RectTransform playButtonRect = playButton.GetComponent<RectTransform>();
        playButtonRect.DOMoveX(120f, transitionDuration).SetEase(Ease.OutQuad).SetRelative(true);

        RectTransform exitButtonRect = exitButton.GetComponent<RectTransform>();
        exitButtonRect.DOMoveX(120f, transitionDuration).SetEase(Ease.OutQuad).SetRelative(true);

        logo.DOMoveY(80f, transitionDuration).SetEase(Ease.OutQuad).SetRelative(true);

        yield return new WaitForSeconds(transitionDuration - .6f);

        progressBar.GetComponent<RectTransform>().DOMoveY(-140f, transitionDuration - .3f).SetEase(Ease.OutQuad).SetRelative(true);

        yield return new WaitForSeconds(transitionDuration - .3f);

        StartCoroutine(LoadScene());
    }
        
    public void NewGame()
    {
        StartCoroutine(MoveOutUIElements());
    }

    public void Quit()
    {
        Application.Quit();
    }
}
