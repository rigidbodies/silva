using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesController : MonoBehaviour
{

    public GameObject[] hearts;

    // public variable with getter and setter
    public int Lives { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Lives < 1)
        {
            hearts[0].gameObject.SetActive(false);
        } else if (Lives < 2)
        {
            hearts[1].gameObject.SetActive(false);
        } else if (Lives < 3)
        {
            hearts[2].gameObject.SetActive(false);
        }
    }

    public void Lose1Life()
    {
        Lives -= 1;
    }
}
