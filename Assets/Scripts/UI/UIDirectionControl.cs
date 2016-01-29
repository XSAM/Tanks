using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    public bool m_UseRelativeRotation = true;  


    private Quaternion m_RelativeRotation;     


    private void Start()
    {
        m_RelativeRotation = transform.parent.localRotation;
    }


    private void Update()
    {
		//Debug.Log(m_RelativeRotation);
        if (m_UseRelativeRotation)
            transform.rotation = m_RelativeRotation;
		//Debug.Log(transform.rotation);
    }
}
