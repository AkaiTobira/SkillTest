using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TextTimeLife : MonoBehaviour
{
    public float m_secondsToDestroy        = 0;
    public float m_expectedTimeOfExistance = 3.2f;
    private float m_timeOfExistence        = 0;
    private Animator m_animator;

    void Start(){
        m_animator = GetComponent<Animator>();
        Destroy( gameObject, m_secondsToDestroy );
    }

    void Update(){
        m_timeOfExistence += Time.deltaTime;
        m_animator.SetFloat("timeOnScene", m_timeOfExistence);
        GetComponent<Text>().text = "This text will deaspear in " +  Mathf.Max( m_expectedTimeOfExistance - m_timeOfExistence, 0 ).ToString();
    }
}
