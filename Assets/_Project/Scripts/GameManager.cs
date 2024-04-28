using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("References")]
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public int roundResetWaitTime = 3;

    // Properties
    public int Score { get; private set; }
    public int Lives{get; private set; }
    public int ghostMultiplier { get; private set; } = 1;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (Lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    /// <summary>
    /// When an enemy dies.
    /// </summary>
    /// <param name="ghost"></param>
    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(Score + points);
        ghostMultiplier++;
    }

    /// <summary>
    /// When Player dies.
    /// </summary>
    public void PacmanEaten()
    {
        pacman.gameObject.SetActive(false);
        SetLives(Lives - 1);

        if(Lives > 0)
        {
            Invoke(nameof(ResetState), roundResetWaitTime);
        }
        else
        {
            GameOver();
        }
    }

    /// <summary>
    /// Logic for eating a normal Pellet.
    /// </summary>
    /// <param name="pellet"></param>
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(Score + pellet.points);

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), roundResetWaitTime);
        }
    }

    /// <summary>
    /// Logic for eating a Power Pellet.
    /// </summary>
    /// <param name="powerPellet"></param>
    public void PowerPelletEaten(PowerPellet powerPellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].Frightened.Enable(powerPellet.duration);
        }

        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);
        PelletEaten(powerPellet);
    }

    /// <summary>
    /// Initialization for a New Game.
    /// </summary>
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    /// <summary>
    /// Logic for starting an entirely new round.
    /// </summary>
    private void NewRound()
    {
        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    /// <summary>
    /// Logic for resetting the game objects when Pacman dies.
    /// </summary>
    private void ResetState()
    {
        ResetGhostMultiplier();

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
    }

    /// <summary>
    /// Logic for losing.
    /// </summary>
    private void GameOver()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        Score = score;
    }

    private void SetLives(int lives)
    {
        Lives = lives;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }

    /// <summary>
    /// Checks to see if there are Pellets still in the game.
    /// </summary>
    /// <returns></returns>
    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
}
