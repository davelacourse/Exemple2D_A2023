using UnityEngine;

public class Ennemi : MonoBehaviour
{
    [SerializeField] private float _vitesseEnnemi = 5f;
    [SerializeField] private GameObject _prefabExplosion = default;

    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _vitesseEnnemi);
        
        // Si sors de l'écran replacer dans haut aléatoirement
        if (transform.position.y <= -6f)
        {
            float valeurAleatoire = Random.Range(-9f, 9f);
            transform.position = new Vector3(valeurAleatoire, 8f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            // Actions pour collision avec le laser
            UIManager.Instance.AjouterScore(10);
            Instantiate(_prefabExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Player")
        {
            // Actions pour collision avec le joueur
            Destroy(this.gameObject);
            // Reduire la vie du joueur
            Player.Instance.DommageJoueur();
        }
    }

}
