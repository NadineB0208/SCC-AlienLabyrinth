using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpawnPortal : MonoBehaviour
{
    public GameObject myContainer;
    public GameObject myPortal;
    private Vector3 vec = new Vector3(0.11f, 1.52f, 2.4f);
    private Quaternion quaternion = Quaternion.identity;

    private void OnMouseDown()
    {
        Instantiate(myPortal, vec, quaternion);
        Destroy(myContainer);
    }
}
