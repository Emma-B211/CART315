using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [Header("Attack")]
    public GameObject ProjectilePrefab;
    public Transform firePoint;
    public float fireCooldown = 0.3f;
    
    [Header("Ammo")]
    public int magazineSize = 6;
    public int reserveAmmo = 18;
    public float reloadTime = 1.2f;

    private int currentAmmo;
    private float nextFireTime;
    private bool isReloading;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = magazineSize;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isReloading)
        return;

         if(Input.GetKeyDown(KeyCode.R)){
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0)){
            Shoot();
        }
        
    }

    void Shoot(){
        if (Time.time < nextFireTime)
        return;

        if (currentAmmo <= 0){
            Debug.Log("Out of ammo! Press R to reload.");
            return;
        }
       if (ProjectilePrefab == null)
{
    Debug.LogError(
        "Projectile Prefab is missing on: " + gameObject.name,
        this
    );
    return;
}

if (firePoint == null)
{
    Debug.LogError(
        "Fire Point is missing on: " + gameObject.name,
        this
    );
    return;
}
        Instantiate(
            ProjectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        currentAmmo--;
        nextFireTime = Time.time + fireCooldown;
        Debug.Log(
            "Ammo: "+ currentAmmo +"/" + reserveAmmo
        );
    }
    System.Collections.IEnumerator Reload(){
        if(currentAmmo == magazineSize || reserveAmmo <= 0)
        // yield break;

        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = magazineSize - currentAmmo;
        int ammoToLoad = Mathf.Min(neededAmmo, reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;

        isReloading = false;

        Debug.Log(
            "Reloaded:" + currentAmmo + "/" + reserveAmmo
        );
    }

}
