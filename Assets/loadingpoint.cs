using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingpoint : MonoBehaviour {
	public GUIText total;
	public GUIText coin;
	// Use this for initialization
	void Start () {
		total.text = scoresaver.getscore();
		coin.text = scoresaver.getcoin();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
