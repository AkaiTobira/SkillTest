using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpButton : MonoBehaviour
{
    
    [SerializeField] GameObject m_popUpWindow = null;
    private Animator m_animator;

    void Start() {
        m_animator = m_popUpWindow.GetComponent<Animator>();
    }

    public void ShowPopUpWindow(){
        m_popUpWindow.SetActive(true);
        m_animator.SetBool("isActive", true);
    }

    public void ClosePopUpWindow(){
        m_popUpWindow.SetActive(false);
        m_animator.SetBool("isActive", false);
    }

}
