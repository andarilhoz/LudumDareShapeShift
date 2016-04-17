using UnityEngine;
using System.Collections;

public class LimitadorManager : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z);
	}
}
