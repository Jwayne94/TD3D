using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;

    [HideInInspector] //������ ������ ����
	public float speed;

    public float health = 200;

    public int worth = 10;

    public GameObject deathEffect; //����� ���������� �������� ������ � ������� Die()

    public GameObject RewardTextCanvas;

    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            PlayerStats.Money += worth;

            GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);

            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        ShowRewardText();
        Destroy(gameObject);
    }

    public void ShowRewardText()
    {
        // ������� ����� ������� Canvas RewardText � ��������� ��� � ����������
        GameObject rewardTextCanvas = (GameObject)Instantiate(RewardTextCanvas, transform.position, Quaternion.identity);

        // �������� ������ TextMeshPro �� ����� Canvas
        TextMeshProUGUI rewardText = rewardTextCanvas.GetComponentInChildren<TextMeshProUGUI>();

        // ������ ����� ��� ������� TextMeshPro
        rewardText.text = "+" + worth.ToString() + "$";

        // ��������� �������� ��������� ������� Canvas
        Animator animator = rewardTextCanvas.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Show");

            // ���������� ������ Canvas ����� ��������� ��������
            Destroy(rewardTextCanvas, animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            // ���������� ������ Canvas ����� 1 ������� ����� ��� ���������
            Destroy(rewardTextCanvas, 1f);
        }
    }

}
