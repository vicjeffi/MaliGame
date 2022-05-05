using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMoverAnimation : MonoBehaviour
{
    Vector3 newCamPosition;

    [SerializeField] Animator CamAnimator;
    [SerializeField] Animator PlayerAnimator;

    GameObject player;

    bool goToSide = false;
    bool isCameraMove = true;

    Vector3 oldPlayerPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SvapCameraMode();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            goToSide = !goToSide;
            if (goToSide)
            {
                CamAnimator.SetBool("GoToSide", true);
            }
            else
            {
                CamAnimator.SetBool("GoToSide", false);
            }
        }
        if (isCameraMove == false)
        {
            oldPlayerPosition = player.transform.position;
            
        }
        if (isCameraMove)
        {
            // тут все нахуй переделать надо чтоб сохранялось растояние определенное и он перемещался только по нему ВОООТ
            this.transform.position = oldPlayerPosition;
        }
    }
    public void PlayerDead()
    {
        PlayerAnimator.SetBool("canPlay", false);
    }

    public void SvapCameraMode()
    {
        isCameraMove = !isCameraMove;
    }
}
