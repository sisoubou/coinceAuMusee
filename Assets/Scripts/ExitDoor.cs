using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [Header("Paramètres d'ouverture")]
    public float vitesseOuverture = 2f;
    public Vector3 distanceOuverture = new Vector3(2.5f, 0, 0); 
    
    private Vector3 positionInitiale;
    private Vector3 positionFinale;
    private bool doitSouvrir = false;

    void Start()
    {
        positionInitiale = transform.position;
        positionFinale = positionInitiale + distanceOuverture;
    }

    void Update()
    {
        if (doitSouvrir)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionFinale, vitesseOuverture * Time.deltaTime);
        }
    }

    public void OuvrirPorte()
    {
        doitSouvrir = true;
    }
}