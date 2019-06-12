using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeScript : MonoBehaviour
{
    public Animator animator;
    public int levelToLoad;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
