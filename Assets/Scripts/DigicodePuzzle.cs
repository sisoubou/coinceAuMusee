using UnityEngine;
using TMPro;

public class DigicodePuzzle : MonoBehaviour
{
    public TextMeshProUGUI ecranTexte;
    public string codeSecret = "1430";
    private string codeSaisi = "";

    public PlayerInteraction playerInteraction;

    public void AjouterChiffre(string chiffre)
    {
        if (codeSaisi.Length < 4)
        {
            codeSaisi += chiffre;
            ecranTexte.text = codeSaisi;
        }

        if (codeSaisi.Length == 4)
        {
            VerifierCode();
        }
    }

    void VerifierCode()
    {
        if (codeSaisi == codeSecret)
        {
            ecranTexte.text = "RÉPARÉ !";
            ecranTexte.color = Color.green;
            Invoke("TerminerMiniJeu", 1.5f);
        }
        else
        {
            ecranTexte.text = "ERREUR";
            ecranTexte.color = Color.red;
            Invoke("ReinitialiserEcran", 1f);
        }
    }

    void ReinitialiserEcran()
    {
        codeSaisi = "";
        ecranTexte.text = "----";
        ecranTexte.color = Color.white;
    }

    void TerminerMiniJeu()
    {
        ReinitialiserEcran();
        playerInteraction.FermerMiniJeu();
        
    }
}