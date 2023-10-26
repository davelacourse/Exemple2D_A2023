using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _ennemiPrefab = default;
    [SerializeField] private GameObject _conteneur = default;

    private void Start()
    {
        StartCoroutine(EnnemiCoroutine());
    }

    IEnumerator EnnemiCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 positionAleatoire = new Vector3(Random.Range(-9, 9), 8f, 0f);
            GameObject nouveauEnnemi = Instantiate(_ennemiPrefab, positionAleatoire, Quaternion.identity);
            nouveauEnnemi.transform.parent = _conteneur.transform;
        }
    }

}
