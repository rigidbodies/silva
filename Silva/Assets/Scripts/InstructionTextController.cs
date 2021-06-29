using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionTextController : MonoBehaviour
{
    [SerializeField] private float timeWhenFadeOut = 2.0f;

    private float timeCounter = 0.0f;
    private bool fadingOut = false;

    private Text instructionText;

    // Start is called before the first frame update
    void Start()
    {
        instructionText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        if(timeCounter >= timeWhenFadeOut && !fadingOut)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        fadingOut = true;

        // Let text fade out
        for (float f = 1; f >= 0.0; f -= 0.05f)
        {
            Color c = instructionText.color;
            c.a = f;
            instructionText.color = c;
            yield return new WaitForSeconds(0.1f);
        }

        // Destroy instructionText
        Destroy(this.gameObject);
    }
}
