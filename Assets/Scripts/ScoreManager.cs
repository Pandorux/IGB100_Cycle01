using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance = null;

    [Tooltip("How often the score increases naturally")]
    public float scoreIncreaseRate;

    [Tooltip("The default amount that the score will increase by")]
    public int scoreIncreaseAmount;

    // TODO: does this make sense?
    [Tooltip("What is the maximum amount of digits that the score could be")]
    public int maxScoreLength;

    [Tooltip("How often the score multiplier increases naturally")]
    public float multiplierIncreaseRate;

    [Tooltip("The default amount that the score multiplier will increase by")]
    public float multiplierIncreaseAmount;

    private float _multiplier;
    [HideInInspector()]
    public float multiplier
    {
        get { return _multiplier; }
        set { _multiplier = value; }
    }

    private float _score;
    [HideInInspector()]
    public float score
    {
        get { return _score; }
        set { _score = value; }
    }

    private float _highscore;

    [HideInInspector()]
    public float highscore
    {
        get { return _highscore; }
        set { _highscore = value; }
    }

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    void Start()
    {
        _score = 0;
        multiplier = 1;

        GameManager.instance.UpdateText(GameManager.instance.scoreText, GameManager.instance.FillTextSpace(10, '0', _score.ToString(), false));
        GameManager.instance.UpdateText(GameManager.instance.multiplierText, 'x' + _multiplier.ToString());

        StartCoroutine(ScoreIncreaseOverTime(scoreIncreaseAmount, scoreIncreaseRate));
        StartCoroutine(MultiplierIncreaseOverTime(multiplierIncreaseAmount, multiplierIncreaseRate));
    }

    public void ScoreIncrease(float amount)
    {
        int rounded;

        _score += amount * _multiplier;
        rounded = (int)_score;
        GameManager.instance.UpdateText(GameManager.instance.scoreText, GameManager.instance.FillTextSpace(10, '0', rounded.ToString(), false));
    }

    public void MultiplierIncrease(float amount)
    {
        _multiplier += amount;
        GameManager.instance.UpdateText(GameManager.instance.multiplierText, 'x' + _multiplier.ToString());
    }

    private IEnumerator ScoreIncreaseOverTime(int amount, float increaseRate, float scoreMulti = 1)
    {
        yield return new WaitForSeconds(increaseRate);
        ScoreIncrease(amount);
        StartCoroutine(ScoreIncreaseOverTime(amount, increaseRate));
    }

    private IEnumerator MultiplierIncreaseOverTime(float amount, float increaseRate)
    {
        yield return new WaitForSeconds(increaseRate);
        MultiplierIncrease(amount);
        StartCoroutine(MultiplierIncreaseOverTime(amount, increaseRate));
    }

}
