using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
public class Move : NetworkBehaviour
{
    float speed = 2f;
    Animator anime;
    void Start()
    {
        anime = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!base.IsOwner)
            return;
        var move = new Vector3(Input.GetAxis("Horizontal"),0, 0);
        transform.position += move * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anime.SetBool("Ismove", true);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anime.SetBool("Ismove", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            anime.SetTrigger("Jump");
        }
    }
}
