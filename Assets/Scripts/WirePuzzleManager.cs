using UnityEngine;

public class WirePuzzleManager : MonoBehaviour
{
    public int filsNecessaires = 3;
    private int filsConnectes = 0;
    
    public PlayerInteraction playerInteraction;
    public GameObject oeuvre3D; 

    public void FilConnecte()
    {
        filsConnectes++;
        Debug.Log("Fils connectés : " + filsConnectes + " / " + filsNecessaires);

        if (filsConnectes >= filsNecessaires)
        {
            TerminerPuzzle();
        }
    }

    void TerminerPuzzle()
    {
        Debug.Log("Puzzle des fils RÉUSSI !");
        
        GameManager.instance.AjouterOeuvreReparee();
        
        oeuvre3D.tag = "Untagged";
        
        Invoke("Fermer", 1.0f);
    }

    void Fermer()
    {
        playerInteraction.FermerMiniJeu();
    }
}