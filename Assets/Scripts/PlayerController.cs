using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour

{
    public bool wizardsRules = true;
    public float animSpeed = 1.5f;
    public bool useCurves = true;

    public float useCurvesHeight = 0.5f;

    public float forwardSpeed = 7.0f;
    public float backwardSpeed = 2.0f;
    public float rotateSpeed = 2.0f;
    public float maxCurve = 0.3f;

    private CapsuleCollider col;
    private Rigidbody rb;
    private Vector3 velocity;

    private GameObject mage;
    private GameObject monster;
    private GameObject player;
    private float h;

    private GameObject gManagerObject;
    private GameManager gManager;
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        player = this.gameObject;

        monster = player.transform.Find("Mutante").gameObject;
        mage    = player.transform.Find("Mago").gameObject;
        gManagerObject = GameObject.Find("GameController");
        gManager = gManagerObject.GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
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
        else if((player.transform.rotation.y >= -maxCurve && direita)||(esquerda && player.transform.rotation.y <= maxCurve))
        {
            h = Input.GetAxis("Horizontal");
            transform.Rotate(0, h * rotateSpeed, 0);
        }

        float v = 1f;
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

        

        if (Input.GetKeyDown(KeyCode.Space)) {
            wizardsRules = !wizardsRules;
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

    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Vibes") {
            Destroy(collision.gameObject);
            gManager.getOrb();
        }
    }
}
