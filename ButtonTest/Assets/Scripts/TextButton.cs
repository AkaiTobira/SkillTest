using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TextButton : MonoBehaviour
{

    public Text m_textPrefab;
    public Transform m_textShowArea;
    public float m_borderOffsetHorizontal;
    public float m_borderOffsetVertical;

    private Vector3 GetRandomPositionInShowArea(){
        Vector3[] v = new Vector3[4];
        m_textShowArea.GetComponent<RectTransform>().GetLocalCorners(v);
        return new Vector3( Random.Range(v[0].x + m_borderOffsetHorizontal, v[2].x - m_borderOffsetHorizontal), 
                            Random.Range(v[0].y + m_borderOffsetVertical,   v[2].y - m_borderOffsetVertical), 0 );
    }

    public void OnButtonPress(){
        Text tempTextBox = Instantiate(m_textPrefab, GetRandomPositionInShowArea(), m_textShowArea.rotation );
        tempTextBox.transform.SetParent(m_textShowArea, false);
    }

}
