
using UnityEngine;

public class TreeHealth : MonoBehaviour
{
    public int treeHealth = 5;

    public void TakeDamage(int damage)
    {
        treeHealth -= damage;

        Debug.Log("Tree health: " + treeHealth);

        if (treeHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Tree has been chopped down!");
        }
    }
}
