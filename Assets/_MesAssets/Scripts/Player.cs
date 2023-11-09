using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player Instance;  // Mise en place Singleton
    
    [Header("Propriétes Joueur")]
    [SerializeField] private int _viesJoueur = 3;
    [SerializeField] private float _vitesse = 7f;
    [SerializeField] private GameObject _laserJoueur = default;
    [SerializeField] private AudioClip _sonLaser = default;
    [SerializeField] private GameObject _tripleLaserJoueur = default;
    [SerializeField] private float _cadenceTir = 0.5f;

    private float _cadenceInitiale;
    private float _peutTire = -1f;
    private bool _tripleLaserActif = false;
    private GameObject _bouclier;
    private Animator _anim;

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
        _cadenceInitiale = _cadenceTir;
        _bouclier = transform.GetChild(0).gameObject;
        _bouclier.SetActive(false);
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Mouvements();
        GestionAnim();
        TirerLaser();

    }

    private void GestionAnim()
    {
        if(Input.GetAxis("Horizontal") < 0)
        {
            _anim.SetBool("Turn_Left", true);
            _anim.SetBool("Turn_Right", false);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", true);
        }
        else
        {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", false);
        }

    }
    
    private void TirerLaser()
    {
        
        if (Input.GetKey(KeyCode.Space) && Time.time > _peutTire)
        {
            AudioSource.PlayClipAtPoint(_sonLaser, Camera.main.transform.position, 0.4f);
            if (!_tripleLaserActif)
            {
                Instantiate(_laserJoueur, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
            }
            else
            {
                Instantiate(_tripleLaserJoueur, transform.position + new Vector3(0f, .5f, 0f), Quaternion.identity);
            }
            
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
        if (!_bouclier.activeSelf)
        {
            _viesJoueur--;
            UIManager.Instance.ChangeLivesDisplayImage(_viesJoueur);
        }
        else
        {
            _bouclier.SetActive(false);
        }

        if (_viesJoueur < 1)
        {
            Destroy(this.gameObject);
            SpawnManager.Instance.FinPartie();
        }
    }

    public void PowerTripleShot()
    {
        _tripleLaserActif = true;
        StartCoroutine(TSCoroutine());
    }

    IEnumerator TSCoroutine()
    {
        yield return new WaitForSeconds(5f);
        _tripleLaserActif = false;
    }

    public void SpeedPowerUp()
    {
        _cadenceTir = 0.1f;
        StartCoroutine(SpeedCoroutine());
    }

    IEnumerator SpeedCoroutine()
    {
        yield return new WaitForSeconds(8f);
        _cadenceTir = _cadenceInitiale;
    }

    public void ShieldPowerUp()
    {
        _bouclier.SetActive(true);
    }


}
