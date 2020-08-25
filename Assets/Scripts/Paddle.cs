using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] int paddleMinXPos = 1;
    [SerializeField] int paddleMaxXPos = 15;

    //cached references
    GameSession myGameSession;
    Ball myBall;

    private void Start()
    {
        myGameSession = FindObjectOfType<GameSession>();
        myBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlesPos = new Vector2(transform.position.x, transform.position.y);
        paddlesPos.x = Mathf.Clamp(GetXPos(), paddleMinXPos, paddleMaxXPos);
        transform.position = paddlesPos;
    }

    private float GetXPos() 
    {
        if (myGameSession.IsAutoPlayEnabled())
        {
            return myBall.transform.position.x;
        }
        else 
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
