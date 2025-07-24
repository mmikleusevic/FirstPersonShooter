using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Parameters")] 
    [SerializeField] private float singleFireCooldown = 0.5f;
    [SerializeField] private float betweenBurstFireShotsCooldown = 0.1f;
    [SerializeField] private float burstFireCooldown = 0.5f;
    [SerializeField] private float rapidFireCooldown = 0.1f;
    [SerializeField] private int burstShotsCount = 3;
    [SerializeField] private float reloadCooldown = 1f;
    [SerializeField] private int maxMagazineSize = 30;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float startFOV = 30;
    [SerializeField] private float endFOV = 60;
    [SerializeField] private float scopingDuration = 0.5f;
    
    [SerializeField] private TextMeshProUGUI fireModeText;
    [SerializeField] private TextMeshProUGUI magazineSizeText;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Bullet bulletPrefab;
    
    private Camera mainCamera;
    
    private FireMode fireMode = FireMode.Single;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private int currentMagazineSize;
    private bool isShooting;
    private bool isReloading;
    private bool isEmpty;
    private bool isScoping;
    //reload, scope, skins, firemode

    private void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera) mainCamera.fieldOfView = startFOV;
        
        startPosition = transform.localPosition;
        endPosition = new Vector3(0, startPosition.y, startPosition.z);
        currentMagazineSize = maxMagazineSize;
        
        SetFireModeText();
        SetMagazineSizeText();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isShooting && !isEmpty && !isReloading)
        {
            StartCoroutine(Fire());
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeFireMode();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && maxAmmo > 0 && currentMagazineSize < maxMagazineSize)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetMouseButtonDown(1) && !isScoping)
        {
            StartCoroutine(ToggleScope());
        }
    }

    private IEnumerator Fire()
    {
        isShooting = true;
        
        switch (fireMode)
        {
            case FireMode.Single:
                yield return StartCoroutine(HandleSingleFire());
                break;
            case FireMode.Burst:
                yield return StartCoroutine(HandleBurstFire());
                break;
            case FireMode.Rapid:
                yield return StartCoroutine(HandleRapidFire());
                break;
            default:
                yield return StartCoroutine(HandleSingleFire());
                break;
        }

        isShooting = false;
    }

    private IEnumerator HandleSingleFire()
    {
        CreateBullet();
        
        yield return new WaitForSeconds(singleFireCooldown);
    }
    
    private IEnumerator HandleBurstFire()
    {
        for (int i = 0; i < burstShotsCount; i++)
        {
            if (isEmpty) break;
            
            CreateBullet();
            
            yield return new WaitForSeconds(betweenBurstFireShotsCooldown);
        }
        
        yield return new WaitForSeconds(burstFireCooldown);
    }
    
    private IEnumerator HandleRapidFire()
    {
        CreateBullet();
        
        yield return new WaitForSeconds(rapidFireCooldown);
    }

    private void ChangeFireMode()
    {
        int fireModeInt = (int)fireMode;
        int length = Enum.GetValues(typeof(FireMode)).Length;
        fireMode = (FireMode)((fireModeInt + 1) % length);

        SetFireModeText();
    }

    private void SetFireModeText()
    {
        fireModeText.text = "FireMode: " + fireMode;
    }
    
    private void SetMagazineSizeText()
    {
        magazineSizeText.text = $"Magazine: {currentMagazineSize + "/" + maxAmmo}";
    }

    private void SetNoAmmoText()
    {
        magazineSizeText.text = "No Ammo...";
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        if (maxAmmo == 0 && isEmpty)
        {
            SetNoAmmoText();
            isReloading = false;
            yield break;
        }
        
        magazineSizeText.text = "Reloading...";
        
        yield return new WaitForSeconds(reloadCooldown);

        int ammoNeeded = maxMagazineSize - currentMagazineSize;
        int ammoToLoad = Mathf.Min(ammoNeeded, maxAmmo);

        currentMagazineSize += ammoToLoad;
        maxAmmo -= ammoToLoad;

        SetMagazineSizeText();
        
        isEmpty = false;
        isReloading = false;
    }

    private IEnumerator ToggleScope()
    {
        isScoping = true;
        float timer = 0f;
        
        while (timer < scopingDuration)
        {
            float elapsed = timer / scopingDuration;
            
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, elapsed);
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsed);
            
            timer += Time.deltaTime;
            
            yield return new WaitForEndOfFrame();
        }
        
        (endFOV, startFOV) = (startFOV, endFOV);
        (startPosition, endPosition) = (endPosition, startPosition);
        
        isScoping = false;
    }

    private void CreateBullet()
    {
        Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        
        currentMagazineSize--;
        
        SetMagazineSizeText();
        
        if (currentMagazineSize == 0) isEmpty = true;
        if (isEmpty && maxAmmo == 0) SetNoAmmoText();
    }
}