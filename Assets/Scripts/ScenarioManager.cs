using UnityEngine;
using System.Collections;

public class ScenarioManager : MonoBehaviour {
    public GameObject ground;
    public GameObject groundBG;
    public Vector3 size;

    public float minOffset;
    public float maxOffset;

    public float maxLarg;
    public float minLarg;

    public float maxCenario;

    private BoxCollider groundCollider;
    private int grounds;
    private int bgGrounds;

    private GameObject[] arvoresDireita;
    private GameObject[] arvoresEsquerda;

    private float distancia;
    // Use this for initialization
    void Start () {
        groundCollider = ground.gameObject.GetComponent<BoxCollider>();
        size = groundCollider.size;
        arvoresDireita = GameObject.FindGameObjectsWithTag("RightTree");
        arvoresEsquerda = GameObject.FindGameObjectsWithTag("LeftTree");
    }
	
	// Update is called once per frame
	void Update () {
        grounds = GameObject.FindGameObjectsWithTag("Ground").Length;
        bgGrounds = GameObject.FindGameObjectsWithTag("GroundBG").Length;


        if (grounds <= 17) {
            createPlatform();
            criaCenarioAmplo();
        }

        distancia = 0f;
        
    }

    void createPlatform() {
        transform.position += new Vector3(0, 0, size.x);
        Instantiate(ground, this.transform.position, transform.rotation);

        for (var i = 0; i < 8; i++)
        {
            int escolhida = Random.Range(0, arvoresDireita.Length );
            GameObject arvore = (GameObject)arvoresDireita[escolhida];

            distancia += Random.Range(minOffset, maxOffset);

            float largeDistance = Random.Range(minLarg, maxLarg);
            Instantiate(arvore, new Vector3(2f + largeDistance, 1f, this.transform.position.z-size.x + distancia), Quaternion.identity);
        }

        for (var i = 0; i < 8; i++)
        {
            int escolhida = Random.Range(0, arvoresEsquerda.Length - 1);
            GameObject arvore = (GameObject)arvoresEsquerda[escolhida];

            distancia += Random.Range(minOffset, maxOffset);
            float largeDistance = Random.Range(minLarg, maxLarg);

            Instantiate(arvore, new Vector3(-2f - largeDistance, 1f, this.transform.position.z - size.x + distancia), Quaternion.identity);
        }

    }

    void criaCenarioAmplo() {
        Instantiate(groundBG, new Vector3(3f,-0.2f,this.transform.position.z), Quaternion.identity);
        for (var i = 0; i < 8; i++)
        {
            int escolhida = Random.Range(0, arvoresDireita.Length - 1);
            GameObject arvore = (GameObject)arvoresDireita[escolhida];

            distancia += Random.Range(minOffset, maxOffset);

            float largeDistance = Random.Range(minLarg, maxCenario);
            Instantiate(arvore, new Vector3(3f + largeDistance, 1f, this.transform.position.z - size.x + distancia), Quaternion.identity);
        }

        for (var i = 0; i < 8; i++)
        {
            int escolhida = Random.Range(0, arvoresEsquerda.Length - 1);
            GameObject arvore = (GameObject)arvoresEsquerda[escolhida];

            distancia += Random.Range(minOffset, maxOffset);
            float largeDistance = Random.Range(minLarg, maxCenario);

            Instantiate(arvore, new Vector3(-3f - largeDistance, 1f, this.transform.position.z - size.x + distancia), Quaternion.identity);
        }

    }

}
