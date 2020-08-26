using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // Start is called before the first frame update
    private Image m_thresholdImage;
    private Image m_touchImage;
    [HideInInspector]
    public Vector3 m_moveDirection;


    public bool m_canShoot;

    private PlayerSpaceship m_player;
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 l_position = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_thresholdImage.rectTransform, eventData.position, eventData.pressEventCamera, out l_position))
        {
            l_position.x = (l_position.x / m_thresholdImage.rectTransform.sizeDelta.x);
            l_position.y = (l_position.y / m_thresholdImage.rectTransform.sizeDelta.y);

            float l_xPos = (m_thresholdImage.rectTransform.pivot.x == 1f) ? l_position.x * 2 + 1 : l_position.x * 2 - 1;
            float l_yPos = (m_thresholdImage.rectTransform.pivot.y == 1f) ? l_position.y * 2 + 1 : l_position.y * 2 - 1;


            m_moveDirection = new Vector3(l_xPos, l_yPos, 0);
            //if the image is on the max distance, input dir is 1 
            m_moveDirection = (m_moveDirection.magnitude > 1) ? m_moveDirection.normalized : m_moveDirection;
            m_touchImage.rectTransform.anchoredPosition = new Vector3(m_moveDirection.x * m_thresholdImage.rectTransform.sizeDelta.x * 0.75f,
                m_moveDirection.y * m_thresholdImage.rectTransform.sizeDelta.y * 0.75f);


            if (m_canShoot)
            {
                m_player.m_canShoot = true;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {


        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!m_canShoot)
            m_moveDirection = Vector3.zero;
        else
            m_player.m_canShoot = false;


        m_touchImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    void Start()
    {
        m_player = FindObjectOfType<PlayerSpaceship>();
        m_thresholdImage = GetComponent<Image>();
        m_touchImage = transform.GetChild(0).GetComponent<Image>();
        m_moveDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
