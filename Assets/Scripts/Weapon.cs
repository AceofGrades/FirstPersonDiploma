using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public int damage = 10, maxReserve = 500, maxClip = 30;
    public float spread = 2f, recoil = 1f, range = 10f, shootRate = .2f;
    public Transform shotorigin;
    public GameObject bulletPrefab;
    public bool canShoot = false;
    public bool takeBullets;

    public static Text ammoText;

    public static int currentClip = 0;


    private int currentReserve = 0;
    private float shootTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Reload();
        //ammoText = GameObject.Find("Bullet Count").GetComponent<Text>();
        currentClip = 0;
        //UpdateBullets(0);
    }

    // Update is called once per frame
    void Update()
    {

        // Increase the shoot timer
        shootTimer += Time.deltaTime;
        // Check if the shoot timer reaches the rate
        if (shootTimer >= shootRate)
        {
            // Can Shoot!
            canShoot = true;
        }
    }

    public void Reload()
    {
        // If there are bullets left in reserve
        if (currentReserve > 0)
        {
            // If there are enough bullets in reserve to fill clip
            if (currentReserve >= maxClip)
            {
                // Reduce the clip size by offset from the current clip to maxClip
                int offset = maxClip - currentClip;
                currentReserve -= offset;
            }
            // If clip is below max clip
            if (currentClip < maxClip)
            {
                int tempMag = currentReserve;
                currentClip = tempMag;
                currentReserve -= tempMag;
            }
        }
    }

    public void Shoot()
    {
        if (currentClip > 0)
        {
            // Reset shoot timer
            shootTimer = 0f;
            // Reset canShoot
            canShoot = false;
            // Get origin + direction of fire
            Camera attachedCamera = Camera.main;
            Transform camTransform = attachedCamera.transform;
            Vector3 lineOrigin = shotorigin.position;
            Vector3 direction = camTransform.forward;
            // Shoot bullet
            GameObject clone = Instantiate(bulletPrefab, camTransform.position, camTransform.rotation);
            Bullet bullet = clone.GetComponent<Bullet>();
            bullet.Fire(lineOrigin, direction);

            //UpdateBullets(-1);
        }
    }

    /*public static void UpdateBullets(int amount)
    {
        currentClip += amount;
        ammoText.text = "Bullets: " + currentClip.ToString();
    }*/
}
