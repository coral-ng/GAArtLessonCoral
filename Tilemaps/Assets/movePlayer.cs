using UnityEngine;
using System.Collections;

public class movePlayer : MonoBehaviour
{
    Animator anim;
    public float speed = 5f;
    private Vector2 movement;

    bool isDiagonal = false;
    bool noDelayStarted = false;
    public float delay = 0.05f;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        //GetComponent<Rigidbody2D>().velocity = movement;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(inputX, inputY);

        //Diagonals

        if (inputX != 0 && inputY != 0)
        {
            isDiagonal = true;
            if (movement.y == 1 && movement.x == -1)
            {
                anim.SetTrigger("move_up_left");
            }

            if (movement.y == 1 && movement.x == 1)
            {
                anim.SetTrigger("move_up_right");
            }

            if (movement.y == -1 && movement.x == -1)
            {
                anim.SetTrigger("move_down_left");
            }

            if (movement.y == -1 && movement.x == 1)
            {
                anim.SetTrigger("move_down_right");
            }
        }
        else
        {

            if (isDiagonal && !noDelayStarted)
            {
                StartCoroutine(NoMoreDiagonal());
                noDelayStarted = true;
            }
            else
            {

                //left/right/up/down
                if (movement.x == -1)
                {
                    anim.SetTrigger("move_left");
                }

                if (movement.x == 1)
                {
                    anim.SetTrigger("move_right");
                }


                if (movement.y == 1)
                {
                    anim.SetTrigger("move_up");
                }


                if (movement.y == -1)
                {
                    anim.SetTrigger("move_down");
                }
            }
        }

        transform.Translate(movement * speed * Time.deltaTime);
    }

    IEnumerator NoMoreDiagonal()
    {
        yield return new WaitForSeconds(delay);
        isDiagonal = false;
        noDelayStarted = false;
    }
}