using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMAIController : MonoBehaviour
{
    public Animator anim; //cria um livro de regras para controlar os movimentos do personagem
    public NavMeshAgent agent; //vamos conectar um personagem para calcular o melhor caminho para um objetivo evitando obstáculos. 
    public Transform player; //Se não me engano isso faz movimentar o personamgem através do espeço 
    public AudioSource audioSource;//Som

    public float visionDistance = 10.0f;//Atribuimos uma distância de visão
    public float visionAngle = 30.0f;// Angulo da visão ou abertura
    public float attackDistance = 7.0f;// atacar de uma certa distância

    private void Awake() // Valores iniciais as variáveis do script
    {
        agent = GetComponent<NavMeshAgent>(); //Vamos buscar o componente do agente
        anim = GetComponent<Animator>(); 
        audioSource = GetComponent<AudioSource>();
    }

    public bool CanSeePlayer()//Aqui é se o soldado ver o Player
    {
        Vector3 direction = player.position - transform.position;
        /*Nesse ponto estamos calculando a distância entre dois pontos no 3D;
         * player.position: É a posição do objeto que chamamos de Player
         * transform.position: É a posição no qual esse escript esta associado. No nosso caso "olhando" para o player
         * A subtração desses dois pontos calcula a distância entre o Player e o Soldado no qual o script esta associado. 
         * */
        float angle = Vector3.Angle(direction, transform.forward);//Faremos uma rotação do soldado ao player 
        if(direction.magnitude < visionDistance && angle < visionAngle)//Se a distância do Soldado e do Player for menor que a distância da visão e o angulo de rotação for menor que a visão de abertura
        {
            return true; //Ele viu
        }
        return false;
    }

    public bool IsPlayerBehind() //Player escondido
    {
        Vector3 direction = transform.position - player.position;//Nesse caso é player se aparecer no campo de visão do soldado a ideia é muito parecida com o primeiro 
        float angle = Vector3.Angle(direction, transform.forward);
        if(direction.magnitude < 2f && angle < visionAngle)
        {
            return true;
        }
        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - transform.position; //Aqui é a mesma loógica de ver o Player 
        if(direction.magnitude < attackDistance) // Mas aqui me da a permissão de atacar.
        {
            return true;
        }
        return false;
    }
}
