using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMAIController : MonoBehaviour
{
    public Animator anim; //cria um livro de regras para controlar os movimentos do personagem
    public NavMeshAgent agent; //vamos conectar um personagem para calcular o melhor caminho para um objetivo evitando obst�culos. 
    public Transform player; //Se n�o me engano isso faz movimentar o personamgem atrav�s do espe�o 
    public AudioSource audioSource;//Som

    public float visionDistance = 10.0f;//Atribuimos uma dist�ncia de vis�o
    public float visionAngle = 30.0f;// Angulo da vis�o ou abertura
    public float attackDistance = 7.0f;// atacar de uma certa dist�ncia

    private void Awake() // Valores iniciais as vari�veis do script
    {
        agent = GetComponent<NavMeshAgent>(); //Vamos buscar o componente do agente
        anim = GetComponent<Animator>(); 
        audioSource = GetComponent<AudioSource>();
    }

    public bool CanSeePlayer()//Aqui � se o soldado ver o Player
    {
        Vector3 direction = player.position - transform.position;
        /*Nesse ponto estamos calculando a dist�ncia entre dois pontos no 3D;
         * player.position: � a posi��o do objeto que chamamos de Player
         * transform.position: � a posi��o no qual esse escript esta associado. No nosso caso "olhando" para o player
         * A subtra��o desses dois pontos calcula a dist�ncia entre o Player e o Soldado no qual o script esta associado. 
         * */
        float angle = Vector3.Angle(direction, transform.forward);//Faremos uma rota��o do soldado ao player 
        if(direction.magnitude < visionDistance && angle < visionAngle)//Se a dist�ncia do Soldado e do Player for menor que a dist�ncia da vis�o e o angulo de rota��o for menor que a vis�o de abertura
        {
            return true; //Ele viu
        }
        return false;
    }

    public bool IsPlayerBehind() //Player escondido
    {
        Vector3 direction = transform.position - player.position;//Nesse caso � player se aparecer no campo de vis�o do soldado a ideia � muito parecida com o primeiro 
        float angle = Vector3.Angle(direction, transform.forward);
        if(direction.magnitude < 2f && angle < visionAngle)
        {
            return true;
        }
        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - transform.position; //Aqui � a mesma lo�gica de ver o Player 
        if(direction.magnitude < attackDistance) // Mas aqui me da a permiss�o de atacar.
        {
            return true;
        }
        return false;
    }
}
