using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup screenOverlay;
    [SerializeField] private float fadeSpeed = 2;
    [SerializeField] private GameObject raceOverPanel;
    [SerializeField] private int nextLevelIndex = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenOverlay.gameObject.SetActive(true);
        raceOverPanel.SetActive(false);
        StartCoroutine(FadeOutOverlay());
    }

    private void OnEnable()
    {
        FinishGate.FinishRace += OnRaceFinished;
    }

    private void OnDisable()
    {
        FinishGate.FinishRace -= OnRaceFinished;
    }

    private void OnRaceFinished()
    {
        raceOverPanel.SetActive(true);
    }

    private IEnumerator FadeOutOverlay()
    {
        while (screenOverlay.alpha > 0)
        {
            screenOverlay.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    private IEnumerator FadeInOverlay()
    {
        while (screenOverlay.alpha < 1)
        {
            screenOverlay.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    public void Restart()
    {
        StartCoroutine(RestartCoroutine());
    }

    public IEnumerator RestartCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelCoroutine());
    }

    public IEnumerator NextLevelCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void Quit()
    {
        StartCoroutine(QuitCoroutine());
    }

    public IEnumerator QuitCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        Application.Quit();
    }
}
