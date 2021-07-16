using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesController : MonoBehaviour
{

    [SerializeField] private GameObject[] hearts;
    public int lives { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives < 1)
        {
            hearts[0].gameObject.SetActive(false);
        } else if (lives < 2)
        {
            hearts[1].gameObject.SetActive(false);
        } else if (lives < 3)
        {
            hearts[2].gameObject.SetActive(false);
        }
    }

    public void Lose1Life()
    {
        lives -= 1;
    }
}
