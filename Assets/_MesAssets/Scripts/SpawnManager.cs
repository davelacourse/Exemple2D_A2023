using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    
    [SerializeField] private GameObject _ennemiPrefab = default;
    [SerializeField] private GameObject _conteneur = default;
    [SerializeField] private GameObject[] _listePowerUps;

    private bool _arretSpawn = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void Start()
    {
        StartCoroutine(EnnemiCoroutine());
        StartCoroutine(PUCoroutine());
    }

    IEnumerator PUCoroutine()
    {
        while (!_arretSpawn)
        {
            yield return new WaitForSeconds(Random.Range(10.0f, 15.0f));
            Vector3 positionAleatoire = new Vector3(Random.Range(-9, 9), 8f, 0f);
            int PUAleatoire = Random.Range(0, _listePowerUps.Length);
            GameObject newPU = Instantiate(_listePowerUps[PUAleatoire], positionAleatoire,
                Quaternion.identity);
            
            
        }
    }

    IEnumerator EnnemiCoroutine()
    {
        while (!_arretSpawn)
        {
            yield return new WaitForSeconds(3f);
            Vector3 positionAleatoire = new Vector3(Random.Range(-9, 9), 8f, 0f);
            GameObject nouveauEnnemi = Instantiate(_ennemiPrefab, positionAleatoire, Quaternion.identity);
            nouveauEnnemi.transform.parent = _conteneur.transform;
        }
    }

    public void FinPartie()
    {
        _arretSpawn = true;
    }

}
