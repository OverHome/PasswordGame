using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    void Start()
    {
        scrollRect.verticalNormalizedPosition = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
