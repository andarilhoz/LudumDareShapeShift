using UnityEngine;
using System.Collections;

public class DestroyerManager : MonoBehaviour {

    private Object[] listAllObjects;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    listAllObjects = GameObject.FindObjectsOfType(typeof(GameObject));

        foreach (Object go in  listAllObjects ) {
            GameObject g = (GameObject) go;
            if (g.transform.position.z < this.transform.position.z) {
                if(g.layer != 5)
                    Destroy(g);              
            }
        }
    }
}
