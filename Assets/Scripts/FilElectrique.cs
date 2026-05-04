using UnityEngine;
using UnityEngine.UI; // Permet de modifier les éléments d'interface comme les couleurs

public class FilElectrique : MonoBehaviour
{
    private bool estConnecte = false;
    public WirePuzzleManager puzzleManager;

    public void ClicSurFil()
    {
        if (!estConnecte)
        {
            estConnecte = true;
            
            GetComponent<Image>().color = Color.green; 
            
            puzzleManager.FilConnecte();
        }
    }
}