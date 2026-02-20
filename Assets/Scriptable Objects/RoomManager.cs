using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    [Header("Load Settings")]
    public bool useAsync = true;
    public float minLoadTime = 0.15f; // prevents instant flash

    bool isLoading;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadRoom(string sceneName)
    {
        if (isLoading) return;
        StartCoroutine(LoadRoomRoutine(sceneName));
    }

    IEnumerator LoadRoomRoutine(string sceneName)
    {
        isLoading = true;

        // OPTIONAL: call your fade-out UI here
        // yield return FadeOut();

        float startTime = Time.time;

        if (!useAsync)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = true;

            while (!op.isDone)
                yield return null;
        }

        // Ensure a tiny minimum time (helps if you add fades)
        float elapsed = Time.time - startTime;
        if (elapsed < minLoadTime)
            yield return new WaitForSeconds(minLoadTime - elapsed);

        // OPTIONAL: call your fade-in UI here
        // yield return FadeIn();

        isLoading = false;
    }
}