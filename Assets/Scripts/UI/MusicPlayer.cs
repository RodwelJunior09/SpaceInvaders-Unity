using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        int musicPlayerCount = FindObjectsByType<MusicPlayer>(FindObjectsSortMode.None).Length;
        if (musicPlayerCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}
