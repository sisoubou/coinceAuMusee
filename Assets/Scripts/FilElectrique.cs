using UnityEngine;
using UnityEngine.EventSystems;

public class FilElectrique : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public LineRenderer lineRenderer;
    public RectTransform pointArrivee;

    public WirePuzzleManager manager;
    
    private bool estConnecte = false;

    void Start()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (estConnecte) return;

        lineRenderer.SetPosition(0, transform.position);

        Vector3 positionSouris = Input.mousePosition;
        positionSouris.z = 1f;
        Vector3 positionMonde = Camera.main.ScreenToWorldPoint(positionSouris);

        lineRenderer.SetPosition(1, positionMonde);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (estConnecte) return;

        Vector3 positionEcranArrivee = Camera.main.WorldToScreenPoint(pointArrivee.position);

        float distance = Vector2.Distance(Input.mousePosition, positionEcranArrivee);

        if (distance < 50f)
        {
            estConnecte = true;
            lineRenderer.SetPosition(1, pointArrivee.position); 
            Debug.Log("Un fil connecté !");
        }
        if (manager != null)
        {
            manager.FilConnecte();
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}