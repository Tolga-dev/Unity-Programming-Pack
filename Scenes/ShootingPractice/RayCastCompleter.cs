using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.ShootingPractice
{
    public class RayCastCompleter : MonoBehaviour
    {
        [Header("Gun Features")]
        public int gunDamage = 2;
        public float fireRate = .24f;
        public float weaponRange = 50f; 
        public float hitForce = 100f;
        private readonly WaitForSeconds _shotDuration = new WaitForSeconds(.07f);
        private AudioSource _gunAudio;
        

        [Header("Camera Features")]
        private Camera _fpsCam;
        
        [Header("Ray Features")]
        public Transform gunEnd;
        public LineRenderer laserLine;
        RaycastHit hit;

        [Header("General Person Features")]
        private float _nextFire;

        private void Start()
        {
            _gunAudio = gameObject.AddComponent<AudioSource>();
            _fpsCam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetButtonDown ("Fire1") && Time.time > _nextFire)
            {
                _nextFire = Time.time + fireRate;
                
                StartCoroutine (ShotEffect());

                Vector3 rayOrigin = _fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));
                

                laserLine.SetPosition (0, gunEnd.position);

                if (Physics.Raycast (rayOrigin, _fpsCam.transform.forward, out hit, weaponRange))
                {
                    laserLine.SetPosition (1, hit.point);
                }
                else
                {
                    laserLine.SetPosition (1, rayOrigin + (_fpsCam.transform.forward * weaponRange));
                }

            }
        }
        private IEnumerator ShotEffect()
        {
            _gunAudio.Play();
            laserLine.enabled = true;

            yield return _shotDuration;

            laserLine.enabled = false;
        }
    }
}