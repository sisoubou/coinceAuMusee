using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Progression")]
    public int oeuvresReparees = 0;
    public int oeuvresTotales = 2;
    public bool jeuEnCours = true; 

    [Header("Inventaire")]
    public bool aTournevis = false; 

    [Header("Chronomètre")]
    public float tempsRestant = 10f; 
    public TextMeshProUGUI texteChrono; 

    [Header("UI")]
    public TextMeshProUGUI texteProgression;
    public TextMeshProUGUI texteVictoire; 
    public GameObject boutonRecommencer;

    [Header("Éléments du niveau")]
    public ExitDoor porteDuMusee;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        MettreAJourUI();
        if (texteVictoire != null) texteVictoire.gameObject.SetActive(false);
        if (boutonRecommencer != null) boutonRecommencer.SetActive(false);
    }

    void Update()
    {
        if (jeuEnCours)
        {
            if (tempsRestant > 0)
            {
                tempsRestant -= Time.deltaTime;
                AfficherTemps(tempsRestant);
            }
            else
            {
                tempsRestant = 0;
                AfficherTemps(0); 
                DeclencherDefaite();
            }
        }
    }

    void AfficherTemps(float tempsAConvertir)
    {
        if (texteChrono == null) return;
        int minutes = Mathf.FloorToInt(tempsAConvertir / 60);
        int secondes = Mathf.FloorToInt(tempsAConvertir % 60);
        texteChrono.text = string.Format("{0:00}:{1:00}", minutes, secondes);
    }

    public void AjouterOeuvreReparee()
    {
        if (!jeuEnCours) return;
        oeuvresReparees++;
        MettreAJourUI();
        if (oeuvresReparees >= oeuvresTotales) DeclencherVictoire();
    }

    void MettreAJourUI()
    {
        if (texteProgression != null) texteProgression.text = "Œuvres restaurées : " + oeuvresReparees + " / " + oeuvresTotales;
    }

    void DeclencherVictoire()
    {
        jeuEnCours = false;
        if (texteVictoire != null)
        {
            texteVictoire.gameObject.SetActive(true);
            texteVictoire.color = Color.green;
            texteVictoire.text = "MUSÉE SAUVÉ !";
        }
        TerminerPartie();
        if (porteDuMusee != null) porteDuMusee.OuvrirPorte();
    }

    void DeclencherDefaite()
    {
        jeuEnCours = false;
        if (texteVictoire != null)
        {
            texteVictoire.gameObject.SetActive(true);
            texteVictoire.color = Color.red;
            texteVictoire.text = "TEMPS ÉCOULÉ !";
        }
        TerminerPartie();
    }

    void TerminerPartie()
    {
        if (boutonRecommencer != null) boutonRecommencer.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RecommencerJeu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}