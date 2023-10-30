using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player Instance;  // Mise en place Singleton
    
    [Header("Propriétes Joueur")]
    [SerializeField] private int _viesJoueur = 3;
    [SerializeField] private float _vitesse = 7f;
    [SerializeField] private GameObject _laserJoueur = default;
    [SerializeField] private float _cadenceTir = 0.5f;

    private float _peutTire = -1f;

    [Header("Limites Jeu")]
    [SerializeField] private float _maxY = 2.5f;
    [SerializeField] private float _minY = -3.8f;
    [SerializeField] private float _valeurX = 11.3f;

    private void Awake()
    {
        if(Instance == null)
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
        _viesJoueur = 3;
    }

    private void Update()
    {
        Mouvements();
        TirerLaser();

    }

    private void TirerLaser()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _peutTire)
        {
            Instantiate(_laserJoueur, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
            _peutTire = Time.time + _cadenceTir;
            
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

    public void DommageJoueur()
    {
        _viesJoueur--;
        UIManager.Instance.ChangeLivesDisplayImage(_viesJoueur);
        if (_viesJoueur < 1)
        {
            Destroy(this.gameObject);
            SpawnManager.Instance.FinPartie();
        }
    }
}
