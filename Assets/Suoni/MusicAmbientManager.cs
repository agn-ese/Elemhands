using UnityEngine;

public class MusicAmbientManager : MonoBehaviour
{
    public AudioSource musicaStarting; // Musica di inizio
    public AudioSource musicaDrone; // Drone musicale sempre attivo
    public AudioSource musicaEnding; // Musica di fine
    public AudioSource mareSound; // Suono del mare
    public AudioSource forestaSound; // Suono della foresta

    public Transform player; // Il giocatore
    public Transform centroIsola; // Il centro dell'isola
    public Transform centroForesta; // Il centro della foresta

    private float volumeMare = 0.8f; // Volume del mare
    private float volumeForesta = 1f; // Volume della foresta, più basso all'inizio
    private float volumeMinimoMare = 0.3f; // Volume minimo del mare (quando lontano)
    private float distanzaRiduzione = 20f; // Distanza per iniziare a ridurre il volume del mare e aumentare quello della foresta
    private float distanzaAttivaForesta = 30f; // Distanza per cominciare a sentire la foresta
    private float volumeMusicaStarting = 0.4f; // Volume della musica di inizio
    private float volumeMusicaEnding = 0.4f; // Volume della musica di fine
    private float volumeMusicaDrone = 0.3f; // Volume del drone

    private bool musicaStartingFinita = false;
    private bool musicaEndingFinita = false;

    void Start()
    {
        musicaDrone.volume = volumeMusicaDrone; // Impostiamo il volume del drone
        musicaDrone.Play(); // Il drone parte sempre
        musicaStarting.volume = volumeMusicaStarting; // Impostiamo il volume della musica di inizio
        musicaStarting.Play(); // La musica di inizio parte
        forestaSound.volume = 0; // Inizia con il volume della foresta a 0
    }

    void Update()
    {
        float distanzaCentroIsola = Vector3.Distance(player.position, centroIsola.position);
        float distanzaCentroForesta = Vector3.Distance(player.position, centroForesta.position);

        // Gestione del volume del mare
        if (distanzaCentroIsola < distanzaRiduzione)
        {
            mareSound.volume = Mathf.Lerp(mareSound.volume, volumeMinimoMare, Time.deltaTime * 2);
        }
        else
        {
            mareSound.volume = Mathf.Lerp(mareSound.volume, volumeMare, Time.deltaTime * 2);
        }

        // Gestione del volume della foresta
        if (distanzaCentroForesta < distanzaAttivaForesta)
        {
            // Aumenta gradualmente il volume della foresta in base alla distanza
            forestaSound.volume = Mathf.Lerp(forestaSound.volume, volumeForesta, Time.deltaTime * 2);
            mareSound.volume = Mathf.Lerp(mareSound.volume, volumeMare, Time.deltaTime / 4); //Diminuisci il volume del mare
        }
        else
        {
            forestaSound.volume = Mathf.Lerp(forestaSound.volume, 0, Time.deltaTime * 2);
        }

        // Gestione della musica di inizio e della ripetizione
        if (!musicaStarting.isPlaying && !musicaStartingFinita)
        {
            musicaStartingFinita = true;
            // Dopo 20 secondi, la musica di inizio riparte se il giocatore è ancora sulla spiaggia
            if (distanzaCentroIsola > distanzaRiduzione)
            {
                Invoke("RipartiMusicaInizio", 20f); // Riparte la musica di inizio dopo 20 secondi
            }
        }

        // Gestione della musica di fine
        if (musicaStartingFinita && !musicaEnding.isPlaying && distanzaCentroForesta < distanzaAttivaForesta)
        {
            musicaEnding.volume = volumeMusicaEnding; // Impostiamo il volume della musica di fine
            musicaEnding.Play();
            musicaDrone.volume = 0.2f; // Abbassiamo ulteriormente il volume del drone mentre la musica di fine suona
        }

        // Se la musica di fine è terminata, la musica di fine riparte dopo 20 secondi
        if (musicaEnding.isPlaying && musicaEnding.time >= musicaEnding.clip.length)
        {
            musicaEndingFinita = true;
            if (musicaEndingFinita)
            {
                Invoke("RipartiMusicaFine", 20f); // Riparte la musica di fine dopo 20 secondi
            }
        }
    }

    void RipartiMusicaInizio()
    {
        musicaStarting.Play();
    }

    void RipartiMusicaFine()
    {
        musicaEnding.Play();
    }
}




