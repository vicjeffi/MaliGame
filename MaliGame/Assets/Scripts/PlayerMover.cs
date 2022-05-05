using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class PlayerMover : MonoBehaviour
{
    Vector3 moucePosition;
    Vector3 whereToPush;
    Vector3 wayTestStartPosition;
    string[] FinishText = { "Lettssss goo!", "Nice coooge!", "Finish!", "Nice one!", "Good job!", "Amaizing!", "U WIN!", "Impressive!", "001011010011011011", "Nice!"};
    string[] DeadText = { "U DEAD!", "HAHAHAH LOSER!", "sOoooO BAD!", "HAHAHAHA NOT EAVEN CLOSE", "sck bools", "sooo cringe"};

    [SerializeField] LayerMask layer;
    [SerializeField] Material[] randomMaterials;
    [SerializeField] Rigidbody r;
    [SerializeField] Text textStatus;
    [SerializeField] Text textR2Restart;

    [SerializeField] Material FreezeMaterial;
    [SerializeField] Material StandartMaterial;
    [SerializeField] ParticleSystem FreezeParticle;
    [SerializeField] ParticleSystem DeadParticle;

    [SerializeField] GameObject wayTest;

    //animators
    [SerializeField] Animator CamAnimator;
    [SerializeField] Animator PlayerAnimator;
    //...

    //static player speed for reset
    public static int NormalSpeed = 2;
    public int speed = NormalSpeed;
    //...

    //bool
    bool isFreeze = false;
    bool isGameFinished = false;
    public bool canPlay = true;
    //...

    public int FreezePower = 2;

    void Start()
    {
        wayTestStartPosition = wayTest.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && canPlay)
        {
            wayTest.transform.position = wayTestStartPosition;
        }
        //reset scene
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (isGameFinished)
        {
            if (Input.anyKey &&!Input.GetMouseButton(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        //...
    }
    private void FixedUpdate()
    {
        //player mover
        if (Input.GetMouseButton(0) && canPlay)
        {
            Ray moucePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(moucePosition, out RaycastHit raycastHit, float.MaxValue, layer))
            {
                whereToPush = raycastHit.point;
                //whereToPush.y = 0.5f;
                wayTest.transform.position = raycastHit.point;
                r.AddForce((whereToPush - transform.position).normalized * speed);
            }
        }
        
        //...
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("Falling"))
        {

        }
        if (this.CompareTag("Player") && other.CompareTag("Killing"))
        {
            if (canPlay)
            {
                canPlay = false;
                DeadParticle.Play();
                textStatus.text = DeadText[Random.Range(0, FinishText.Length - 1)];
                if (isGameFinished == false)
                    CamAnimator.SetBool("canPlay", false);
                PlayerAnimator.SetBool("canPlay", false);
                Destroy(wayTest);
            }
        }
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
            //change colider text and set bool
            textStatus.text = FinishText[Random.Range(0, FinishText.Length - 1)];
            textR2Restart.text = "Press any key";

            textR2Restart.color = GetComponent<Renderer>().material.color;
            textStatus.color = GetComponent<Renderer>().material.color;

            isGameFinished = true;
            CamAnimator.SetBool("canPlay", false);
            canPlay = false;
            //...
        }
        if (this.CompareTag("Player") && other.CompareTag("Freeze"))
        {
            //freeze
            if (isFreeze == false)
            {
                speed = speed / FreezePower;
                this.GetComponent<Renderer>().material = FreezeMaterial;
                FreezeParticle.Play();
                isFreeze = true;
            }
            //...
        }
        if (this.CompareTag("Player") && other.CompareTag("Booster"))
        {
            //unfreeze
            if (isFreeze)
            {
                speed = speed * FreezePower;
                this.GetComponent<Renderer>().material = StandartMaterial;
                FreezeParticle.Play();
                isFreeze = false;
            }
            //...

            //smaller;
            speed += 4;
            if((r.drag - 0.5f) >= 0)
            {
                r.drag -= 0.5f;
            }
            Destroy(FindClosestObjByTag("Booster"));
            //...
        }
        if (this.CompareTag("Player") && other.CompareTag("UnBooster"))
        {
            //unfreeze
            if (isFreeze)
            {
                speed = speed * FreezePower;
                this.GetComponent<Renderer>().material = StandartMaterial;
                FreezeParticle.Play();
                isFreeze = false;
            }
            //...

            //bigger;
            //add mass and lower speed
            r.drag += 0.5f;

            int local_coof = 4;

            tryAgain:
            if ((speed - local_coof) > 0)
            {
                speed -= local_coof;
            }
            else
            {
                local_coof -= 1;
                goto tryAgain;
            }
            
            //...
            Destroy(FindClosestObjByTag("UnBooster"));
            //...
        }
    }

    public GameObject FindClosestObjByTag(string tag)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
