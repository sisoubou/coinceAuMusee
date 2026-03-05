using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Progression")]
    public int oeuvresReparees = 0;
    public int oeuvresTotales = 1;

    [Header("UI")]
    public TextMeshProUGUI texteProgression;
    public TextMeshProUGUI texteVictoire;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        MettreAJourUI();
        if (texteVictoire != null) texteVictoire.gameObject.SetActive(false);
    }

    public void AjouterOeuvreReparee()
    {
        oeuvresReparees++;
        MettreAJourUI();

        if (oeuvresReparees >= oeuvresTotales)
        {
            DeclencherVictoire();
        }
    }

    void MettreAJourUI()
    {
        if (texteProgression != null)
        {
            texteProgression.text = "Œuvres restaurées : " + oeuvresReparees + " / " + oeuvresTotales;
        }
    }

    void DeclencherVictoire()
    {
        Debug.Log("Bravo ! Toutes les œuvres sont restaurées !");
        if (texteVictoire != null)
        {
            texteVictoire.gameObject.SetActive(true);
            texteVictoire.text = "MUSÉE SAUVÉ !\nLa porte est ouverte.";
        }
    }
}