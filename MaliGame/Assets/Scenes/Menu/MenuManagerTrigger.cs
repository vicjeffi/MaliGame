using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator MenuCamAnimator;
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
            MenuCamAnimator.SetBool("isActive", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this && other.CompareTag("Player"))
        {
            MenuCamAnimator.SetBool("isActive", false);
        }
    }
}
