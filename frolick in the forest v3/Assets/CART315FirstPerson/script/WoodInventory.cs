
using UnityEngine;

public class WoodInventory : MonoBehaviour
{
    public int woodCount = 0;

    public void AddWood(int amount)
    {
        woodCount += amount;
        Debug.Log("Wood collected: " + woodCount);
    }

    public bool UseWood(int amount)
    {
        if (woodCount < amount)
        {
            Debug.Log("Not enough wood!");
            return false;
        }

        woodCount -= amount;
        return true;
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 22;
        style.normal.textColor = Color.white;
        style.fontStyle = FontStyle.Bold;

        GUI.Label(
            new Rect(20, 20, 250, 40),
            "Wood: " + woodCount,
            style
        );
    }
}
