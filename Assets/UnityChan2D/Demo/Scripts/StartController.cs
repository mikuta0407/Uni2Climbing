// git hub test;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class StartController : MonoBehaviour{
    [SceneName]
    public string nextLevel;

    [SerializeField]
    // private KeyCode enter = KeyCode.X;
    private string worldName;
    void Update(){

        if (Input.GetKeyDown(KeyCode.F)){
            Screen.fullScreen = !Screen.fullScreen;
        }
        
        if ((Input.GetKeyDown(KeyCode.Return)) || (Input.GetKeyDown ("joystick button 7"))) { //Enter押されたら
            worldName = "Loading 1-1";          //次読むこむのをLoading1-1にして(デバッグ用に選べるようにしていた名残)
            nowworld.setworld("1-1");           //nowworldで1-1にセットして
            StartCoroutine(LoadStage());        //シーン読み込みを開始。
            scoresaver.setcoin("00");
            scoresaver.setscore("0000000");
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

    private IEnumerator LoadStage(){
        //多分まず音楽流して、音楽の長さ+0.5したらシーンチェンジしてるんじゃないですかね?
        
        foreach (AudioSource audioS in FindObjectsOfType<AudioSource>()){
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
