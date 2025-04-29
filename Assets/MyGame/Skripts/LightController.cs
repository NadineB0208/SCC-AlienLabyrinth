using UnityEngine;
using System.Collections;
using System.Text;

public class LightController : MonoBehaviour
{
    public Light lamp;
    public float dotDuration = 0.2f;
    public float dashDuration = 0.6f;
    public float symbolPauseDuration = 0.2f;
    public float wordPauseDuration = 0.8f;
    public string messageToTransmit = "Hallo Du";

    private void Start()
    {
        if (lamp == null)
        {
            Debug.LogError("Keine Lichtkomponente zugewiesen!");
            enabled = false;
        }
        StartCoroutine(BlinkMessageInMorse(messageToTransmit.ToUpper()));
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
                        yield return new WaitForSeconds(dotDuration);
                    }
                    else if (symbol == '-')
                    {
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