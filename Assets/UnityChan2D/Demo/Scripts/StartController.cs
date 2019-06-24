// git hub test;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class StartController : MonoBehaviour
{
    [SceneName]
    public string nextLevel;

    [SerializeField]
    // private KeyCode enter = KeyCode.X;
    private string worldName;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            worldName = "Loading 1-1";
            nowworld.setworld("1-1");
            StartCoroutine(LoadStage());
        }
        /*    if (Input.GetKeyDown(KeyCode.Alpha1)) {
            worldName = "Loading 1-1";
            nowworld.setworld("1-1");
            StartCoroutine(LoadStage());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            worldName = "Loading 1-2";
            StartCoroutine(LoadStage());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            worldName = "Loading 1-3";
            StartCoroutine(LoadStage());
        }
        */
    }

    private IEnumerator LoadStage()
    {
        foreach (AudioSource audioS in FindObjectsOfType<AudioSource>())
        {
            audioS.volume = 0.2f;
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;

        audioSource.Play();
        
        count.sethit(0);
        count.setlife(5);
        yield return new WaitForSeconds(audioSource.clip.length + 0.5f);
        SceneManager.LoadScene (worldName);
        //Application.LoadLevel(nextLevel);
    }
}
