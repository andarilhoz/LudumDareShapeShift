using UnityEngine;
using System.Collections;

public class ItenManager : MonoBehaviour {

    public GameObject vibes;
    private int vibez;
    public float maxDistance;
    public float minDistance;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        vibez = GameObject.FindGameObjectsWithTag("Vibes").Length;
        if (vibez <= 50) {
            float acrescenta = Random.Range(minDistance, maxDistance);
            this.transform.position += new Vector3(0, 0, acrescenta);
            Instantiate(vibes, new Vector3(Random.Range(-1.9f,2),0.5f,this.transform.position.z), Quaternion.identity);
        }

    }
}
