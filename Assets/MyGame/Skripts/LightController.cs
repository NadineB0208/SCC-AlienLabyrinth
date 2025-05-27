using UnityEngine;
using System.Collections;
using System.Text;
using Unity.VisualScripting;

public class LightController : MonoBehaviour
{
    public Light lamp;
    public float dotDuration = 0.5f;
    public float dashDuration = 1f;
    public float symbolPauseDuration = 0.3f;
    public float wordPauseDuration = 1.3f;
    public string messageToTransmit = "Daten";
    private void Start()
    {
        if (lamp == null)
        {
            Debug.LogError("Keine Lichtkomponente zugewiesen!");
            enabled = false;
        }
        // StartCoroutine(BlinkMessageInMorse(messageToTransmit.ToUpper()));
    }

    private void OnMouseDown()
    {
        StopAllCoroutines();
        StartCoroutine(BlinkMessageInMorse(messageToTransmit.ToUpper()));
        Debug.Log("Play Morse");
    }
    private IEnumerator BlinkMessageInMorse(string message)
    {
        StringBuilder morseCodeText = new StringBuilder();
        foreach (char letter in message)
        {
            if (MorseAlphabet.MorseCode.ContainsKey(letter))
            {
                string morse = MorseAlphabet.MorseCode[letter];
                morseCodeText.Append(morse).Append(" "); // Füge ein Leerzeichen zwischen den Symbolen hinzu

                foreach (char symbol in morse)
                {
                    lamp.enabled = true;
                    if (symbol == '.')
                    {
                        lamp.color = Color.red;
                        yield return new WaitForSeconds(dotDuration);
                    }
                    else if (symbol == '-')
                    {
                        lamp.color = Color.blue;
                        yield return new WaitForSeconds(dashDuration);
                    }
                    lamp.enabled = false;
                    yield return new WaitForSeconds(symbolPauseDuration);
                }
            }
            else
            {
                Debug.LogWarning($"Zeichen '{letter}' nicht im Morsecode-Alphabet gefunden.");
            }
            yield return new WaitForSeconds(wordPauseDuration); // Pause zwischen Wörtern
        }
        Debug.Log("Morsecode: " + morseCodeText.ToString().Trim());
    }
}