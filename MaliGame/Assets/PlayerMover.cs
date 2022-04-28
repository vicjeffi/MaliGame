using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class PlayerMover : MonoBehaviour
{
    Vector3 moucePosition;
    Vector3 whereToPush;
    Vector3 newCamPosition;

    [SerializeField] LayerMask layer;
    [SerializeField] Material[] randomMaterials;
    [SerializeField] Rigidbody r;
    
    [SerializeField] Material FreezeMaterial;
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
    [SerializeField] bool isCameraMove = true;
    bool isFreeze = false;
    bool isGameFinished = false;
    public bool canPlay = true;
    //...

    public int FreezePower = 2;

    private Material MyMaterialNow;
    void Start()
    {
        //set random material
        Color MyColorNow = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1.0f); 
        this.GetComponent<Renderer>().material.color = MyColorNow;
        var MyMaterialNow = this.GetComponent<Renderer>().material;
        //...
    }

    void Update()
    {
        //camera mover
        if (isCameraMove)
        {
            newCamPosition.x = this.transform.position.x + 8;
            newCamPosition.z = this.transform.position.z - 8;
            newCamPosition.y = Camera.main.transform.position.y;
            Camera.main.transform.position = newCamPosition;
        }
        //...

        //reset scene
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            canPlay = false;
            DeadParticle.Play();
            CamAnimator.SetBool("canPlay", false);
            PlayerAnimator.SetBool("canPlay", false);
            Destroy(wayTest);
        }
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
            //new scene
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
                this.GetComponent<Renderer>().material = MyMaterialNow;
                FreezeParticle.Play();
                isFreeze = false;
            }
            //...

            //smaller;
            this.transform.localScale = this.transform.lossyScale / 2;
            speed += 4;
            if((r.drag - 0.5f) >= 0)
            {
                r.drag -= 0.5f;
            }
            GameObject b = GameObject.FindGameObjectWithTag("Booster");
            Destroy(FindClosestObjByTag("Booster"));
            //...
        }
        if (this.CompareTag("Player") && other.CompareTag("UnBooster"))
        {
            //unfreeze
            if (isFreeze)
            {
                speed = speed * FreezePower;
                this.GetComponent<Renderer>().material = MyMaterialNow;
                FreezeParticle.Play();
                isFreeze = false;
            }
            //...

            //bigger;
            this.transform.localScale = this.transform.lossyScale * 2;
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
            this.GetComponent<Renderer>().material = MyMaterialNow;
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

    public void PlayerDead()
    {
        canPlay = false;
        DeadParticle.Play();
        PlayerAnimator.SetBool("canPlay", false);
    }
}
