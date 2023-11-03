using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBanner : MonoBehaviour
{
    private Vector3 startPos;
    private Image objectImage;
    private float timer;
    public bool down;
    // Start is called before the first frame update
    void Start()
    {
        objectImage = GetComponent<Image>();
        startPos = objectImage.rectTransform.anchoredPosition;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.05)
        {
            if (down)
            {
                objectImage.rectTransform.anchoredPosition += Vector2.down * 25;
            }
            else
            {
                objectImage.rectTransform.anchoredPosition += Vector2.up * 25;
            }
            timer = 0;
        }
        if (Vector2.Distance(objectImage.rectTransform.anchoredPosition, startPos) > 1500)
        {
            objectImage.rectTransform.anchoredPosition = startPos;
        }
    }
}
