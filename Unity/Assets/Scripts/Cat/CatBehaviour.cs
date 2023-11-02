using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CatBehaviour : MonoBehaviour
{
    public GameObject heartParticles;
    public TextMeshProUGUI scoreText;
    public int score = 0;

    public Image affectionMeter;
    public float affection = 0f;
    public float maxAffection = 10f;

    public AudioSource audioSource;
    public AudioSource purAudioSource;
    public AudioClip pain;
    public AudioClip love;
    public AudioClip wow;

    public float gameTimer = 60f;
    bool playing = true;

    public enum CatState
    {

    }

    public Transform owner;

    bool wandering = true;
    bool dead = false;
    public Animator animator;
    public float wanderRadius;
    public float wanderTimer;

    private NavMeshAgent agent;
    private float timer;

    public Vector3 previousPosition;
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        timer = wanderTimer;
    }

    public TextMeshProUGUI timerText;
    private bool HasReachedDestination()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }
        return false;
    }

    public void Death()
    {
        if(dead)
        {
            return;
        }
        IEnumerator DeathCoroutine()
        {
            agent.isStopped = true;
            score = (int)(score * 0.5f);
            animator.SetTrigger("Death");
            dead = true;

            yield return new WaitForSeconds(3f);
            dead = false;
            agent.isStopped = false;
            animator.SetTrigger("Idle");

        }
        StartCoroutine(DeathCoroutine());
    }
    public void GoToOwner()
    {
        wandering = false;
        agent.SetDestination(owner.position);
    }

    public void Bounce()
    {
        IEnumerator Anim()
        {
            wandering = false;
            animator.SetTrigger("Bounce");
            agent.isStopped = true;
            yield return new WaitForSeconds(1f);
            agent.isStopped = false;
            wandering = true;
        }
        StartCoroutine(Anim());
    }

    float lastTouch = 0f;

    public void AddAffection(float rate)
    {
        if(dead)
        {
            return;
        }

        if(lastTouch < 1.5f)
        {
            audioSource.PlayOneShot(love);
        }
        lastTouch = 2f;

        affection += Time.deltaTime * rate;
    }

    float scoreCooldown = 0f;
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public GameObject gameOverPanel;
    void Update()
    {
        if(!playing)
        {
            return;
        }
        if(scoreCooldown > 0f)
        {
            scoreCooldown -= Time.deltaTime;
        }
        gameTimer -= Time.deltaTime;

        timerText.text = "Timer: " + gameTimer.ToString("F2");
        if(gameTimer <= 0)
        {
            playing = false;
            gameOverPanel.SetActive(true);
        }
        if (wandering && affection > 8.5f)
        {
            GoToOwner();
        }
        scoreText.text = "Score: " + score;
        if(lastTouch > 0f)
        {
            lastTouch -= Time.deltaTime;
        }
        if(affection > 0f)
        {
            affection -= Time.deltaTime * (1f - lastTouch/2f);
        }

        if(lastTouch > 1.9f)
        {
            purAudioSource.volume = 1f;
            heartParticles.SetActive(true);
        }
        else
        {
            purAudioSource.volume =0f;
            heartParticles.SetActive(false);
        }

        if (affection > maxAffection)
        {
            Death();
            audioSource.PlayOneShot(pain);
            affection = 0f;
        }
        affectionMeter.fillAmount = affection / maxAffection;

        Vector3 lookRotation = (transform.position - previousPosition).normalized;
        if(lookRotation.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), Time.deltaTime * 2f);
        }

        if (!wandering && !dead)
        {
            if(HasReachedDestination() && scoreCooldown <= 0f)
            {
                GameObject heartParticlesGO = Instantiate(heartParticles);
                heartParticlesGO.transform.position = transform.position;
                heartParticlesGO.transform.localScale = Vector3.one;
                heartParticlesGO.SetActive(true);
                Destroy(heartParticlesGO, 2f);
                audioSource.PlayOneShot(wow);

                scoreCooldown = 3f;
                animator.SetTrigger("Roll");
                score++;
                wandering = true;
                affection = 0f;
            }
        }
        float movementSpeed = (transform.position - previousPosition).magnitude * 100f;
        animator.SetFloat("MovementSpeed", movementSpeed);

        if(wandering && !dead)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(Vector3.zero, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
      
        previousPosition = transform.position;
    }
    public Vector3 ClampMinMagnitude(Vector3 vector, float minMagnitude)
    {
        if (vector.magnitude < minMagnitude)
        {
            vector = vector.normalized * minMagnitude;
        }
        return vector;
    }


    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection = ClampMinMagnitude(randDirection, dist * 0.5f);

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}