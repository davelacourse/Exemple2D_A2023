using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Propriétes Joueur")]
    [SerializeField] private float _vitesse = 7f;
    [SerializeField] private GameObject _laserJoueur = default;

    [Header("Limites Jeu")]
    [SerializeField] private float _maxY = 2.5f;
    [SerializeField] private float _minY = -3.8f;
    [SerializeField] private float _valeurX = 11.3f;


    private void Update()
    {
        Mouvements();
        TirerLaser();

    }

    private void TirerLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject laser = Instantiate(_laserJoueur, transform.position + new Vector3(0f, 0.8f, 0f), Quaternion.identity);
        }
    }

    private void Mouvements()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(hInput, vInput, 0f);
        transform.Translate(direction * Time.deltaTime * _vitesse);

        //Limiter mes positions verticalement
        transform.position = new Vector3(transform.position.x, 
            Mathf.Clamp(transform.position.y, _minY, _maxY), 
            0f);

        //Téportation axe des X gauche/droite
        if (transform.position.x >= _valeurX)
        {
            transform.position = new Vector3(-_valeurX, transform.position.y, 0f);
        }
        else if(transform.position.x <= -_valeurX)
        {
            transform.position = new Vector3(_valeurX, transform.position.y, 0f);
        }
    }
}
