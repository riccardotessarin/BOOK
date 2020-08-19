using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Characters.Interfaces;
namespace Managers{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private Image staminaBar;
        [SerializeField] private PlayableCharacter player;
        [SerializeField] private RawImage equippedAttackImage;
        [SerializeField] private Image inGameObjectMenu;
        [SerializeField] private Texture voidSprite;
        [SerializeField]private RawImage centerObject;
        [SerializeField]private RawImage rightObject;
        [SerializeField]private RawImage leftObject;
        
        
        

        public void FillBar(float value, string type) {
            if(healthBar && staminaBar){
                switch (type) {
                    case "health":
                        healthBar.fillAmount = value;
                        break;
                    case "stamina":
                        staminaBar.fillAmount = value;
                        break;
                    default:
                        break;
                }
            }
        }

       

        void Awake(){
            gameObject.tag="UIManager";
            player=GameObject.FindWithTag("Player").GetComponent<PlayableCharacter>();
            centerObject=inGameObjectMenu.transform.GetChild(0).GetComponent<RawImage>();
            leftObject=inGameObjectMenu.transform.GetChild(1).GetComponent<RawImage>();
            rightObject=inGameObjectMenu.transform.GetChild(2).GetComponent<RawImage>();

        }

        

        

        // Start is called before the first frame update
        void Start()
        {
            equippedAttackImage.texture=player.BaseAttackSprite;
            centerObject.texture=player.BaseAttackSprite;
            rightObject.texture=player.SpecialAttackSprite;
            leftObject.texture=player.SpecialAttackSprite;

        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void ActivateMenu(bool on){
            inGameObjectMenu.gameObject.SetActive(on);
        }

        public void ScrollDownMenu(Texture rightTexture){
            leftObject.texture=centerObject.texture;
            centerObject.texture=rightObject.texture;
            rightObject.texture=rightTexture;
            equippedAttackImage.texture=centerObject.texture;
            
        }
        public void ScrollUpMenu(Texture leftTexture){
            rightObject.texture=centerObject.texture;
            centerObject.texture=leftObject.texture;
            leftObject.texture=leftTexture;
            equippedAttackImage.texture=centerObject.texture;
        }
        
    }
}
