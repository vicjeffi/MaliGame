using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSkript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim_onText;
    public Animator anim_onCube;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this && other.CompareTag("Player"))
        {
            anim_onCube.SetBool("Disapear", true);
            if(anim_onText != null)
                anim_onText.SetBool("Disapear", true);
        }
    }
}
