using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Références")]
    public Camera mainCamera;
    public TextMeshProUGUI interactionText; 
    public FirstPersonController fpsController; 
    
    [Header("Paramètres")]
    public float interactionDistance = 3f;

    private bool isPlayingMiniGame = false;
    private GameObject canvasActif;

    void Update()
    {
        if (isPlayingMiniGame) return;

        if (interactionText != null) interactionText.gameObject.SetActive(false);

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                if (interactionText != null) interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Le joueur cherche maintenant DEUX choses possibles :
                    PuzzleTrigger trigger = hit.collider.GetComponent<PuzzleTrigger>();
                    ItemRamassable item = hit.collider.GetComponent<ItemRamassable>();

                    // 1. Est-ce un puzzle ?
                    if (trigger != null)
                    {
                        if (trigger.necessiteTournevis && !GameManager.instance.aTournevis)
                        {
                            Debug.Log("Il me faut un tournevis pour ouvrir ce boîtier...");
                            if (interactionText != null) interactionText.text = "Il me faut un tournevis !";
                        }
                        else
                        {
                            OuvrirMiniJeu(trigger.canvasDuPuzzle);
                        }
                    }
                    // 2. Est-ce un objet à ramasser (comme le tournevis) ?
                    else if (item != null)
                    {
                        item.Ramasser();
                    }
                    // 3. Sinon, erreur.
                    else 
                    {
                        Debug.LogWarning("Il manque un script (PuzzleTrigger ou ItemRamassable) sur cet objet !");
                    }
                }
            }
        }
    }

    public void OuvrirMiniJeu(GameObject canvasAcheminer)
    {
        isPlayingMiniGame = true;
        if (interactionText != null) interactionText.gameObject.SetActive(false); 
        
        canvasActif = canvasAcheminer;
        canvasActif.SetActive(true); 

        fpsController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void FermerMiniJeu()
    {
        isPlayingMiniGame = false;
        if (canvasActif != null) canvasActif.SetActive(false); 

        fpsController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}