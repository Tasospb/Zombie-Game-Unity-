using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public int maxAmmo = 30; // Bullets per mag
    public int bulletsLeft = 120; // Total bullets we have
    private int currentAmmo; // Bullets we have left in the mag
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Text ammoText;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public New_Weapon_Recoil_Script recoil;

    public Animator animator;

    public AudioSource shootSound;
    public AudioSource ReloadingSound;

    void Start ()
    {
        currentAmmo = maxAmmo;

        UpdateAmmoText(); // Update Ammo text
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && bulletsLeft > 0)
        {
            StartCoroutine(Reload());
        }


        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
            recoil.Fire();
        }

        IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading...");

            animator.SetBool("Reloading", true);

            yield return new WaitForSeconds(reloadTime);

            animator.SetBool("Reloading", false);

            isReloading = false;

            int bulletsToLoad = maxAmmo - currentAmmo;
            int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

            bulletsLeft -= bulletsToDeduct;
            currentAmmo += bulletsToDeduct;

            UpdateAmmoText(); // Update Ammo text
            ReloadingSound.Play(); // Sound for when we are reloading the gun
        }

        void Shoot ()
        {
            muzzleFlash.Play();
            shootSound.Play();

            currentAmmo--;

            UpdateAmmoText(); // Update Ammo text

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if(target != null)
                {
                    target.TakeDamage(damage);
                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }
    }

    public void UpdateAmmoText()
    {
        ammoText.text = currentAmmo + " / " + bulletsLeft;
    }
}
