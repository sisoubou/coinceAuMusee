using UnityEngine;

public class ItemRamassable : MonoBehaviour
{
    public string nomObjet = "Tournevis";

    public void Ramasser()
    {
        if (nomObjet == "Tournevis")
        {
            GameManager.instance.aTournevis = true;
            Debug.Log("J'ai trouvé le tournevis !");
        }
        Destroy(gameObject);
    }
}