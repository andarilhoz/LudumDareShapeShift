using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject.name == "Lua")
            this.transform.Rotate(0, 5 * Time.deltaTime, 0);
        else
            this.transform.Rotate(0, -(5 * Time.deltaTime), 0);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z);
    }
}
