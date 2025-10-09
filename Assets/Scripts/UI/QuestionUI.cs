using UnityEngine;

public class QuestionUI : MonoBehaviour
{
    // Sleep hier je panel naartoe in de inspector
    public GameObject myPanel;

    // Functie om het panel te tonen
    public void ShowPanel()
    {
        if (myPanel != null)
        {
            myPanel.SetActive(true);
        }
    }

    // Functie voor je remune knop
    public void Remune()
    {
        Debug.Log("Remune button pressed!");

        // Verberg het panel als je op remune drukt
        if (myPanel != null)
        {
            myPanel.SetActive(false);
        }

        // Voeg hier eventueel andere remune-logica toe
    }

    // Optioneel: aparte functie om het panel te verbergen (bijv. voor een Close knop)
    public void HidePanel()
    {
        if (myPanel != null)
        {
            myPanel.SetActive(false);
        }
    }
}
