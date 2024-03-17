using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokemonData : MonoBehaviour
{
    // [SerializeField] permet d'afficher la variable/tableau dans l'inspector
    // La variable "pokemonName" est une string qui contient le nom du pokemon (string)
    [SerializeField] string PokemonName = "Umbreon";
    // La variable "PokemonBaseLife" est un int qui contient le vie de base du pokemon
    [SerializeField] int PokemonBaseLife = 95;
    // La variable "PokemonLife" est un int qui contient la vie actuelle du pokemon (initialisé au lancement du jeu)
    [SerializeField] int PokemonLife;
    // La variable "PokemonAtk" est un int qui contient l'attaque du pokemon
    [SerializeField] int PokemonAtk = 65;
    // La variable "PokemonDef" est un int qui contient la défense du pokemon
    [SerializeField] int PokemonDef = 110;
    // La variable "PokemonStats" est un int qui contient la stat du pokemon (initialisé au lancement du jeu)
    [SerializeField] int PokemonStats;
    // La variable "PokemonWeight" est un float qui contient le poids du pokemon
    [SerializeField] float PokemonWeight = 27.0f;
    // Enumeration de tous les types (enum) 
    enum PokemonTypeList { Normal, Fire, Water, Electric, Grass, Ice, Fighting, Poison, Ground, Flying, Psychic, Bug, Rock, Ghost, Dragon, Dark, Steel, Fairy, Stellar };
    // Initialisation du type du pokemon 
    [SerializeField] PokemonTypeList CurrentType = PokemonTypeList.Dark;
    // Tableau contenant les faiblesses du pokemon
    [SerializeField] PokemonTypeList[] PokemonWeaknesseType = {PokemonTypeList.Fighting, PokemonTypeList.Bug, PokemonTypeList.Fairy};
    // Tableau contenant les résistances du pokemon
    [SerializeField] PokemonTypeList[] PokemonResistanceType = {PokemonTypeList.Ghost, PokemonTypeList.Dark};
    // Tableau contenant les immunités du pokemon
    [SerializeField] PokemonTypeList[] PokemonImmuneType = {PokemonTypeList.Psychic};

    // Awake execute les fonctions/methodes à l'intialisation du jeu
    void Awake() {
        InitCurrentLife();
        InitStatsPoints();
    }

    // Start execute les fonctions/methodes au lancement du jeu
    void Start() {
        Display();
        TakeDamage(50,"Normal");
    }

    // Update execute les fonctions/methodes à chaque frame du jeu
    void Update() {
        CheckIfPokemonAlive();
    }

    // La fonction/methode "Display" permet d'afficher toutes les informations du pokemon (nom, pv, atk, def, stats, poids, type, faiblesses, resistances, immunités)
    void Display() {
        Debug.Log("Name : " + PokemonName);
        Debug.Log("HP : " + PokemonBaseLife + " points");
        Debug.Log("Attack : " + PokemonAtk + " points");
        Debug.Log("Defense : " + PokemonDef + " points");
        Debug.Log("Stats : " + PokemonStats + " points");
        Debug.Log("Weight : " + PokemonWeight + " Kg");
        Debug.Log("Type : " + CurrentType);

        for (int i=0; i<PokemonWeaknesseType.Length; i++) {
            Debug.Log("Weaknesses : " + PokemonWeaknesseType[i]);
        }

        for (int i=0; i<PokemonResistanceType.Length; i++) {
            Debug.Log("Resistances : " + PokemonResistanceType[i]);
        }

        for (int i=0; i<PokemonImmuneType.Length; i++) {
            Debug.Log("Immune(s) : " + PokemonImmuneType[i]);
        }
    }
    
    // La fonction/methode "InitCurrentLife" intialise la vie courante du pokemon en fonction de ses pv de base
    void InitCurrentLife() {
        PokemonLife = PokemonBaseLife;
    }
    
    // La fonction/methode "InitStatsPoints" intialise les stats du pokemon en fonction de ses pv de base, son attaque et sa défense
    void InitStatsPoints() {
        PokemonStats += PokemonBaseLife + PokemonAtk + PokemonDef; 
    }
    
    // La fonction/methode "GetAttackDamage" renvoie l'attaque du pokemon
    int GetAttackDamage() {
        return PokemonAtk;
    }

    // La fonction/methode "TakeDamage" prend en paramètres les dégâts à infliger et le type du pokémon ennemi
    // Si le type est dans le tableau "PokemonWeaknesseType" alors le pokemon subira le double des dégats normaux
    // Si le type est dans le tableau "PokemonResistanceType" alors le pokemon subira la moitier des dégats normaux
    // Si le type est dans le tableau "PokemonImmuneType" alors le pokemon ne subira aucun dégat
    // Si le type ne fait parti d'aucun des 3 tableaux alors le pokemon subira les dégats normaux
    // Si les pv du pokemon tombent en dessous de 0 alors, ils seront initalisé à 0
    void TakeDamage(int damage, string pokEnnType) {
        for (int i=0; i<PokemonWeaknesseType.Length; i++) {
            if (pokEnnType == PokemonWeaknesseType[i].ToString()) {
                PokemonLife -= damage * 2;
            }
        }
        
        for (int i=0; i<PokemonResistanceType.Length; i++) {
            if (pokEnnType == PokemonResistanceType[i].ToString()) {
                PokemonLife -= damage / 2;
            }
        }

        for (int i=0; i<PokemonImmuneType.Length; i++) {
            if (pokEnnType == PokemonImmuneType[i].ToString()) {
                PokemonLife += 0;
            } else {
                PokemonLife -= damage;
            }
        }


        if (PokemonLife <= 0) {
            PokemonLife = 0;
        }

        Debug.Log(PokemonName + " have now " + PokemonLife + " HP");
    }

    // La fonction/methode "CheckIfPokemonAlive" vérifie si le pokemon est encore en vie
    // si la fonction/methode est vrai alors le message "[nom_du_pokemon] is still alive" dans la console
    // si la fonction/methode est fausse alors le message "[nom_du_pokemon] is dead" dans la console
    void CheckIfPokemonAlive() {
        if (PokemonLife <= 0) {
            PokemonLife = 0;
            Debug.Log(PokemonName + " is dead");
        } else {
            Debug.Log(PokemonName + " is still alive");
        }
    }
}
