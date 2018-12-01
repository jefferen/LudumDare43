using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    float h, v;
    public GameObject player;
    Vector2 nom = new Vector2(0,0);
    public CircleCollider2D playerHitBox;
    public List<GameObject> moveableTargets = new List<GameObject>();
    IEnumerator input;
	
	void Start ()
    {
        input = WaitingForInput();
        StartCoroutine(input);
	}

    void ExecuteTurn(Vector2 movement)
    {
        StopCoroutine(input);
        // Enemy ai and companions will check the board and perform action based on it!
        // Enemy and companion want to engage in combat either by moving closer or if at attack range will attack
        // all movement will be done with lerp
        for (int i = 0; i < moveableTargets.Count; i++)
        {
            //moveableTargets[1].
        }
        StartCoroutine(Lerp(player, movement));
    }

    void ReadBoard()
    {

    }

    IEnumerator WaitingForInput()
    {
        playerHitBox.enabled = false; // disable all trigger boxes

        while (true)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            if (h > 0) // switch?
            {
                ExecuteTurn(new Vector2(1.4f, 0));
            }
            else if(h < 0)
            {
                ExecuteTurn(new Vector2(-1.4f, 0));
            }
            if (v > 0)
            {
                ExecuteTurn(new Vector2(0, 1.4f));
            }
            else if (v < 0)
            {
                ExecuteTurn(new Vector2(0,-1.4f));
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Attack();
                ExecuteTurn(nom);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                //sacrifice turn
                ExecuteTurn(nom);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                //sacrifice abillity
                ExecuteTurn(nom);
            }

            yield return null;
        }
    }
    void Attack()
    {
        // deal dmg to every one around you
        playerHitBox.enabled = true;
    }

    IEnumerator Lerp(GameObject target, Vector2 movement)
    {
        float t = 0;
        Vector2 startPos = target.transform.position;

        while (t <= 1)
        {
            t += Time.deltaTime;
            target.transform.position = Vector2.Lerp(target.transform.position, 
                new Vector2(startPos.x + movement.x,startPos.y + movement.y), t); // t*10/0.1  t*2/0.5  t/1
            yield return null;
        }
        StartCoroutine(input);
    }
}
