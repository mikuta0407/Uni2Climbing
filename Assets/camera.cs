using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Camera))]
public class camera : MonoBehaviour
{
	public string nextLevel;

    GameObject player;

    // Use this for initialization
    void Start()
    {
        // Playerの部分はカメラが追いかけたいオブジェクトの名前をいれる
        this.player = GameObject.Find("DemoUnityChan2D");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(-1, playerPos.y + 0.5f, transform.position.z);
		transform.rotation = Quaternion.Euler(0,0,0);

		if (playerPos.y + 0.5f > 89){
			StartCoroutine(INTERNAL_Clear());
            enabled = false;
		}
    }
	private IEnumerator INTERNAL_Clear()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            player.SendMessage("Clear", SendMessageOptions.DontRequireReceiver);
        }

        yield return new WaitForSeconds(3);
		SceneManager.LoadScene ("Start");
        // Application.LoadLevel(nextLevel);
    }
}