using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextMap : MonoBehaviour
{
    public bool goneThere = false; // nayttaa ollanko oltu paikassa aiemmin
    public bool canGo = false;     // maaraa voiko menna
    public bool searchNext = false; // maaraa
    public bool exit = false;
    public bool mapTarget = false; //target of a special mission from dialogue
    public int number = 0; //order number in which the islands are moved towards each other
    // public Button yesButton;
    // public Button noButton;

    public Sprite newsprite;
    public Sprite newExitsprite;

    // public Question question;
    public StrangeWaters strangeWaters;
    public bool collisionCheck1 = false;
    public float radius = 3.75f;

    public bool GetcanGo() { return canGo; }
    // Start is called before the first frame update
    void Start()
    {
        //  yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        //noButton = GameObject.Find("NoButton").GetComponent<Button>();
        // yesButton.onClick.AddListener(GoA);
        //  noButton.onClick.AddListener(answerNo);
        //question = FindObjectOfType<Question>();
        //question = gameObject.GetComponent<Question>();
        strangeWaters = FindObjectOfType<StrangeWaters>();

        if (searchNext)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);
            if (colliders.Length == 0)
            {
                while (true)
                {
                    radius++;
                    colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);
                    if (colliders.Length != 0)
                    {
                        break;
                    }
                }
            }

     /*       foreach (var hit in colliders)
            {
                if (hit.gameObject != gameObject)
                {
                    hit.GetComponent<NextMap>().canGo = true;
                    if (hit.tag == "Exit")
                    {
                        hit.GetComponent<SpriteRenderer>().sprite = newExitsprite;
                    }
                    else if(hit.gameObject.GetComponent<NextMap>().goneThere)
                    {
                        //do nothing
                    }
                    else
                    {
                        hit.GetComponent<SpriteRenderer>().sprite = newsprite;
                    }
                }
            } */
        }
    }

    public void CheckForIslandsNear()
    {
        if (searchNext)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);
            if (colliders.Length == 0)
            {
                while (true)
                {
                    radius++;
                    colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);
                    if (colliders.Length != 0)
                    {
                        break;
                    }
                }
            }

            foreach (var hit in colliders)
            {
                if (hit.gameObject != gameObject)
                {
                    hit.GetComponent<NextMap>().canGo = true;
                    if (hit.tag == "Exit")
                    {
                        hit.GetComponent<SpriteRenderer>().sprite = newExitsprite;
                    }
                    else if (hit.gameObject.GetComponent<NextMap>().goneThere)
                    {
                        //do nothing
                    }
                    else
                    {
                        hit.GetComponent<SpriteRenderer>().sprite = newsprite;
                    }
                }
            }
        }
    }

    /*

    public void GoToNext()
    {
       int  RandomNumber = Random.Range(1, 3);
        SceneManager.LoadScene(RandomNumber);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionCheck1 = true;
    }
    private void OnCollisionExit2D(UnityEngine.Collision collision)
    {
        collisionCheck1 = false;
    }
        */

    public void setGoneThere()
    {
        goneThere = true;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //jos on lahella edellista objektia ja ei olla menty sinne aiemmin tulee kysymys
            if (canGo)
            {
             
                    strangeWaters.GetComponent<StrangeWaters>().NewMap(this.gameObject);
                
            }
        }
    }



    /*
    public void GoA()
    {
        if (gameObject.tag == "Exit")
        {
            strangeWaters.GetComponent<StrangeWaters>().CloseCanvas();
            SceneManager.LoadScene("TheBigMap");
        }
        else
        {
            strangeWaters.GetComponent<StrangeWaters>().CloseCanvas();

            goneThere = true;
            GoToNext();
        }
    }

    public void answerNo()
    {
        searchNext = true;
        strangeWaters.GetComponent<StrangeWaters>().CloseCanvas();
    }

*/

    // Update is called once per frame
    void Update()
    {
        // jos tama on true, objeckti etsii lahella olevat objektit ja vaihtaa ne mahdollisiksi menna

    }
}
