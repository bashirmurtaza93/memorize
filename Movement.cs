using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Movement : MonoBehaviour
{
   
    /***********************Initilization goes  here **********************************************/
    List<GameObject> arrows = new List<GameObject>();
    List<GameObject> randomInput = new List<GameObject>();
    List<GameObject> userInput = new List<GameObject>();
    Vector3 targetPosition = new Vector3(500f, 0f, 0f);
    int difficulty = 5;

    /**********************************End Initizaition *********************************************/



    void Update () {
        GameObject left = GameObject.FindGameObjectWithTag("Left");
        GameObject right = GameObject.FindGameObjectWithTag("Right");
        GameObject down = GameObject.FindGameObjectWithTag("Down");
        GameObject up = GameObject.FindGameObjectWithTag("Up");
        arrows.Add(left); //[0]
        arrows.Add(down);//[1]
        arrows.Add(right);//[2]
        arrows.Add(up);//[3]
        startGame();
  
    } // end function


    // Function starts the game. Creating the 
    void startGame()
    {
        
        // here we will get the difficulty. 
        for (int i = 0; i < difficulty; i++)
          randomInput.Add(arrows[random()]);
        //If the user hits the space button then we start the animation. 
        if (Input.GetKeyDown(KeyCode.Space))
           StartCoroutine(arrowAnimate());
    } // end start game function.


    /* arrowAnimate function goes through the the randomInput array,
    and goes through the array by animating and placing it on the
    screen for the user to see. Afterwards we call the userTurn function.
    */

    IEnumerator arrowAnimate() {   
        for (int i = 0; i < difficulty; i++)
        {
            var a = randomInput[i];
            Vector2 pos = Camera.main.ScreenToWorldPoint(transform.position);
            Debug.Log("a is : " + a);
            a.transform.Translate(new Vector2(pos.x,pos.y) * 500f * Time.deltaTime);
            yield return new WaitForSeconds(1);
            a.transform.Translate(Vector2.up * 500f * Time.deltaTime);
        } // end for loop    

        //start userTurn function.
        StartCoroutine(userTurn());
 
    } // end arrow Animate function.





    IEnumerator userTurn(){
        for (int i = 0; i < difficulty; i++)
        {
            var wait = true;
            while (wait)
            {
                //Debug.Log("In user turn");
                GameObject left = GameObject.FindGameObjectWithTag("Left");
                GameObject right = GameObject.FindGameObjectWithTag("Right");
                GameObject down = GameObject.FindGameObjectWithTag("Down");
                GameObject up = GameObject.FindGameObjectWithTag("Up");
                //Switch statement would be far better for this, but w.e.
              
                if (Input.GetKeyDown(KeyCode.A))
                {
                    userInput.Add(left);
                    wait = false;
                    Debug.Log("left");
                    yield return null;

                } // end if


                if (Input.GetKeyDown(KeyCode.W))
                {
                    userInput.Add(up);
                    wait = false;
                    Debug.Log("up");
                    yield return null;
                } // end if
                if (Input.GetKeyDown(KeyCode.S))
                {
                    userInput.Add(down);
                    wait = false;
                    Debug.Log("down");
                    yield return null;
                } // end if
                if (Input.GetKeyDown(KeyCode.D)) 
                {
                    userInput.Add(right);
                    wait = false;
                    Debug.Log("right");
                    yield return null;
                } // end if


                yield return null;
            } // end while loop
            yield return null;
        } // end for loop


        int count = 0;
        for (int i = 0; i < difficulty; i++)
        {
            var a = userInput[i];
            var b = randomInput[i];

            if (userInput[i] == randomInput[i])
            {
                ++count;
            } // end if
            
        } // end for


        userInput.ForEach(x => Debug.Log(x));
        if (count == difficulty)
        {
            Debug.Log("You Win");
            userInput.Clear();
            difficulty++;
        }
        else
        {
            userInput.Clear();
            Debug.Log("You lost");
        }
        
    } // end function


    int random()
    {
        // Function to get random from 0, to difficulty.
        int a = Random.Range(0, (difficulty - 1));
        return (a);
    } // end function


} // end program

