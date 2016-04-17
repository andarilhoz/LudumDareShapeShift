using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour

{
    public bool wizardsRules = true;
    public float animSpeed = 1.5f;
    public bool useCurves = true;

    public float velocidadeMaxima;
    public float velocidadeInicial;

    public float velocidadePlayer = 1f;

    public float useCurvesHeight = 0.5f;

    public float forwardSpeed = 7.0f;
    public float backwardSpeed = 2.0f;
    public float rotateSpeed = 2.0f;
    public float maxCurve = 0.3f;

    public float coolDownShapeShift;

    private CapsuleCollider col;
    private Rigidbody rb;
    private Vector3 velocity;

    private GameObject mage;
    private GameObject monster;
    private GameObject player;
    private float h;

    public float coolDown = 0f;

    private GameObject gManagerObject;
    private GameManager gManager;

    private int alinhamento;

    public GameObject gameOverScreen;
    public GameObject HUDScreen;

    private AudioSource passos;

    private bool isAlive = true;


    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        player = this.gameObject;

        monster = player.transform.Find("Hyde").gameObject;
        mage    = player.transform.Find("Jekyll").gameObject;
        gManagerObject = GameObject.Find("GameController");
        gManager = gManagerObject.GetComponent<GameManager>();
        coolDown = coolDownShapeShift;
        
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gManager.exitGame();
            }

            if (Input.GetKey(KeyCode.Space) && coolDown <= 0)
            {
                wizardsRules = !wizardsRules;
                coolDown = coolDownShapeShift;
            }

            if (wizardsRules)
            {
                monster.SetActive(false);
                mage.SetActive(true);
            }
            else {
                monster.SetActive(true);
                mage.SetActive(false);
            }

            if (Input.GetKey(KeyCode.X))
            {
                AhhMorreuPoOlhaAi();
            }

            coolDown -= Time.deltaTime;

            bool direita = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
            bool esquerda = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));

            if (direita && player.transform.rotation.y <= -maxCurve)
            {
                h = 0f;
            }
            else if (esquerda && player.transform.rotation.y >= maxCurve)
            {
                h = 0f;
            }
            else if ((player.transform.rotation.y >= -maxCurve && direita) || (esquerda && player.transform.rotation.y <= maxCurve))
            {
                h = Input.GetAxis("Horizontal");
                transform.Rotate(0, h * rotateSpeed, 0);
            }

            velocidadePlayer = velocidadeInicial + (transform.position.z / 300);

            if (velocidadePlayer >= velocidadeMaxima)
            {
                velocidadePlayer = velocidadeMaxima;
            }

            float v = velocidadePlayer;
            rb.useGravity = true;

            velocity = new Vector3(0, 0, v);
            velocity = transform.TransformDirection(velocity);
            if (v > 0.1)
            {
                velocity *= forwardSpeed;
            }
            else if (v < -0.1)
            {
                velocity *= backwardSpeed;
            }

            transform.localPosition += velocity * Time.fixedDeltaTime;
        }
        
    }

    void AhhMorreuPoOlhaAi()
    {
        isAlive = false;
        velocidadePlayer = 0f;
        HUDScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        gManager.setScore();
        if (wizardsRules) { 
            passos = GetComponent<AudioSource>();
            passos.Stop();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Vibes") {
            Destroy(collision.gameObject);
            gManager.getOrb();
            if (wizardsRules)
                alinhamento++;
            else
                alinhamento--;
        }
        else if (collision.gameObject.name == "CoolZone")
        {
            if (wizardsRules) {
                
            }
            else {

            }

        }
        else if (collision.gameObject.name == "DangerZone")
        {
            if (wizardsRules) {

            }
            else {

            }
        }
    }

}
