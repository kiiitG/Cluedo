using UnityEngine.UI;
using UnityEngine;

public class TableItem : MonoBehaviour
{
    public void Awake()
    {
        GetComponentInChildren<Text>().text = GetComponent<Card>().GetName();
    }
}
