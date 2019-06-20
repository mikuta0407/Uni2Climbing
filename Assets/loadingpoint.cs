using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingpoint : MonoBehaviour {
	public GUIText total;
	// Use this for initialization
	void Start () {
		total.text = scoresaver.getscore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
