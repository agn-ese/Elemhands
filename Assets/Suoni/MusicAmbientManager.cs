using UnityEngine;
using System.Collections;

public class MusicAmbientManager : MonoBehaviour
{
    public AudioSource musicaDrone; // Musica Drone sempre attiva
    public AudioSource musicaStarting; // Musica1 (spiaggia)
    public AudioSource musicaEnding; // Musica2 (foresta)
    public AudioSource suonoMare; // Suono del Mare
    public AudioSource suonoForesta; // Suono della Foresta

    public Transform player; // Il giocatore
    public Transform centroIsola; // Oggetto vuoto rappresentante il centro dell'isola
    public Transform centroForesta; // Oggetto vuoto rappresentante il centro della foresta

    private AudioSource musicaAttuale = null; // Memorizza la musica in riproduzione

    void Start()
    {
        musicaDrone.loop = true;
        musicaDrone.volume = 1.3f; // Imposta volume leggermente più alto
        musicaDrone.Play();

        StartCoroutine(GestisciMusica());
    }

    IEnumerator GestisciMusica()
    {
        while (true)
        {
            float distanzaDaForesta = Vector3.Distance(player.position, centroForesta.position);
            float distanzaDaIsola = Vector3.Distance(player.position, centroIsola.position);

            // Volume mare: diventa meno forte se ci si avvicina al centro dell'isola
            if (distanzaDaIsola < 50f)
            {
                suonoMare.volume = 1 - distanzaDaIsola / 50f; // Riduce il volume man mano che ci si avvicina
            }
            else
            {
                suonoMare.volume = 0;
            }

            // Riduci ulteriormente il volume del mare quando il giocatore è vicino alla foresta
            if (distanzaDaForesta < 20f)
            {
                suonoMare.volume = Mathf.Clamp01(1 - distanzaDaForesta / 20f) * 0.2f; // Ridurre ulteriormente il volume del mare dentro la foresta
            }

            // Volume foresta: aumenta gradualmente man mano che ci si avvicina alla foresta
            if (distanzaDaForesta < 20f)
            {
                suonoForesta.volume = Mathf.Clamp01(1 - distanzaDaForesta / 20f) * 1.5f; // Aumenta il volume della foresta
            }
            else if (distanzaDaForesta < 50f)
            {
                suonoForesta.volume = Mathf.Clamp01(1 - distanzaDaForesta / 50f);
            }
            else if (distanzaDaForesta < 100f) // Inizia a sentire il suono della foresta da una distanza maggiore
            {
                suonoForesta.volume = 0.5f; // Imposta un volume base a distanza maggiore
            }
            else
            {
                suonoForesta.volume = 0; // Se il giocatore è lontano, nessun suono
            }

            // Determina quale musica riprodurre in base alla posizione
            if (distanzaDaForesta < distanzaDaIsola)
            {
                yield return RiproduciMusica(musicaEnding);
            }
            else
            {
                yield return RiproduciMusica(musicaStarting);
            }

            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator RiproduciMusica(AudioSource nuovaMusica)
    {
        if (musicaAttuale != nuovaMusica)
        {
            if (musicaAttuale != null && musicaAttuale.isPlaying)
            {
                musicaAttuale.Stop();
            }

            musicaAttuale = nuovaMusica;
        }

        musicaAttuale.Play();
        yield return new WaitForSeconds(musicaAttuale.clip.length);
        yield return new WaitForSeconds(4f);
    }
}



