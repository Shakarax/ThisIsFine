using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : WeaponController
{

    private void Start()
    {
        weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        weaponSpriteRenderer.color = ChangingPistolColor();
       
    }

    private Color ChangingPistolColor() // Change Pistol Color Based on Quality Number
    {
        Color pistolColor = new Color32(0,0,0, 255);
        int pistolR;
        int pistolG;
        int pistolB;

        switch (quality) 
        {
            //This to change to Green (Common Quality)
            case 1:
                {
                    pistolR = 105;
                    pistolG = 236;
                    pistolB = 104;

                    pistolColor = new Color32((byte)pistolR, (byte)pistolG, (byte)pistolB, 255);
                    break;
                }
            //This to change to Blue (Rare Quality)   
            case 2 :
                {
                    pistolR = 0;
                    pistolG = 45;
                    pistolB = 255;

                    pistolColor = new Color32((byte)pistolR, (byte)pistolG, (byte)pistolB, 255);
                    break;
                }
                    
            //This is to change to Purple (Epic Quality)
            case 3:
                {
                    pistolR = 198;
                    pistolG = 0;
                    pistolB = 255;

                    pistolColor = new Color32((byte)pistolR, (byte)pistolG, (byte)pistolB, 255);
                    break;
                }

            //This is to change to Gold (Legendary Quality)
            case 4:
                {
                    pistolR = 255;
                    pistolG = 208;
                    pistolB = 0;

                    pistolColor = new Color32((byte)pistolR, (byte)pistolG, (byte)pistolB, 255);
                    break;
                }
        }
        return pistolColor; 
    }


}
