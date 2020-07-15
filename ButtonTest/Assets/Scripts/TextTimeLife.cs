using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TextTimeLife : MonoBehaviour
{
    public float secondsToDestroy = 0;
    private float timeOfExistence = 0;

    private Animator m_animator;

    void Start(){
        m_animator = GetComponent<Animator>();
        Destroy( gameObject, secondsToDestroy );
    }

    void Update()
    {
        timeOfExistence += Time.deltaTime;
        m_animator.SetFloat("timeOnScene", timeOfExistence);
        GetComponent<Text>().text = "This text will deaspear in " +  Mathf.Max( 3.2f - timeOfExistence, 0 ).ToString();
    }
}
