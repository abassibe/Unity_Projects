using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class dragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragOnSurfaces = true;
    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;
    public gameManager gm;
    public GameObject Canon1;
    public GameObject Canon2;
    public GameObject Canon3;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;
        switch (name)
        {
            case "Canon1":
                if (gm.playerEnergy < 80)
                    return;
                break;
            case "Canon2":
                if (gm.playerEnergy < 50)
                    return;
                break;
            case "Canon3":
                if (gm.playerEnergy < 100)
                    return;
                break;
            default:
                break;
        }

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        m_DraggingIcon = new GameObject("icon");

        m_DraggingIcon.transform.SetParent(canvas.transform, false);
        m_DraggingIcon.transform.SetAsLastSibling();

        var image = m_DraggingIcon.AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite;
        image.SetNativeSize();

        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        if (m_DraggingIcon != null)
            SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_DraggingIcon != null)
            Destroy(m_DraggingIcon);
        // DROP STATE
        RaycastHit2D hit;

        if ((hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero)))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.tag == "empty" && !gm.occupedTiles.Contains(hitObject))
            {
                switch (name)
                {
                    case "Canon1":
                        if (gm.playerEnergy >= 80)
                        {
                            gm.playerEnergy -= 80;
                            GameObject.Instantiate(Canon1, hitObject.transform.position, Quaternion.identity);
                            gm.occupedTiles.Add(hitObject);
                        }
                        break;
                    case "Canon2":
                        if (gm.playerEnergy >= 50)
                        {
                            gm.playerEnergy -= 50;
                            GameObject.Instantiate(Canon2, hitObject.transform.position, Quaternion.identity);
                            gm.occupedTiles.Add(hitObject);
                        }
                        break;
                    case "Canon3":
                        if (gm.playerEnergy >= 100)
                        {
                            gm.playerEnergy -= 100;
                            GameObject.Instantiate(Canon3, hitObject.transform.position, Quaternion.identity);
                            gm.occupedTiles.Add(hitObject);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }
}