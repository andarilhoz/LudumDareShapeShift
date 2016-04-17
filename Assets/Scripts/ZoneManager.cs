using UnityEngine;
using System.Collections;

public class ZoneManager : MonoBehaviour
{

    public GameObject dangerZone;
    public GameObject coolZone;

    private int zonas;
    public float maxDistance;
    public float minDistance;
    private int choose;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        zonas = GameObject.FindGameObjectsWithTag("CoolZone").Length + GameObject.FindGameObjectsWithTag("DangerZone").Length;
        if (zonas <= 4)
        {
            choose = Random.Range(0, 2);
            float acrescenta = Random.Range(minDistance, maxDistance);
            this.transform.position += new Vector3(0, 0, acrescenta);
            if(choose == 0)
                Instantiate(coolZone, new Vector3(0f, 0f, this.transform.position.z), Quaternion.identity);
            else
                Instantiate(dangerZone, new Vector3(0f, 0f, this.transform.position.z), Quaternion.identity);
        }

    }
}
