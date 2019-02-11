﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeHandler : SizeHandler {

    public IEnumerator DynamicCameraMovement(float Upperlimit,float LowerLimit)
    {
        float camSize = Camera.main.fieldOfView;

        //Şimdilik 9.5 la falan başlıyor ilk yolun mesafesi. ondan hardcoded değiştirilcek ama sonra
        float roadLenghtUpperLimit = Platform.instance.initialStraightRoadLenght + 4f;
        float roadLengthLowerLimit = Platform.instance.initialStraightRoadLenght - 4f;

        //Debug.Log(Platform.instance.initialStraightRoadLenght);
        float roadMiddleReferencePoint = Platform.instance.initialStraightRoadLenght;

        //float distanceBtwLowBounds = roadMiddleReferencePoint - roadLengthLowerLimit;
        //float distanceBtwUpperBounds = roadLenghtUpperLimit - roadMiddleReferencePoint;

        float camDiffLowBounds = camSize - LowerLimit; // 60 - lower cam size
        float camDiffUpperBounds = Upperlimit - camSize; // upper cam size - 60
        //Debug.Log(camDiffUpperBounds);

        yield return new WaitUntil(() => Platform.instance.game.state == GameHandler.GameState.GameRunning);

        while(Platform.instance.game.state == GameHandler.GameState.GameRunning)
        {
            //Debug.Log(Platform.instance.straightRoadLenght);
            if ((Platform.instance.straightRoadLenght) > roadMiddleReferencePoint) //genişlicekse
            {
                //Debug.Log("genişlicek bu kadar : " + (camDiffUpperBounds * ((Platform.instance.straightRoadLenght - roadMiddleReferencePoint) / (roadLenghtUpperLimit - roadMiddleReferencePoint))));

                float newCamSize  = camSize + (camDiffUpperBounds * ((Platform.instance.straightRoadLenght - roadMiddleReferencePoint) / (roadLenghtUpperLimit - roadMiddleReferencePoint)));

                if(Camera.main.fieldOfView <  newCamSize)
                {
                    Camera.main.fieldOfView += 0.1f;
                }
                else
                {
                    Camera.main.fieldOfView -= 0.1f;
                }

                //Camera.main.fieldOfView = camSize + (camDiffUpperBounds * ((Platform.instance.straightRoadLenght - roadMiddleReferencePoint) / (roadLenghtUpperLimit - roadMiddleReferencePoint)));
            }
            else if (Platform.instance.straightRoadLenght <= roadMiddleReferencePoint) // daralcaksa
            {
                //Debug.Log("daralcak  bu kadar : " + (camDiffLowBounds * ((roadMiddleReferencePoint - Platform.instance.straightRoadLenght) / (roadMiddleReferencePoint - roadLengthLowerLimit))));

                float newCamSize = camSize - (camDiffLowBounds * ((roadMiddleReferencePoint - Platform.instance.straightRoadLenght) / (roadMiddleReferencePoint - roadLengthLowerLimit)));

                if(Camera.main.fieldOfView > newCamSize)
                {
                    Camera.main.fieldOfView -= 0.1f;
                }
                else
                {
                    Camera.main.fieldOfView += 0.1f;
                }

                //Camera.main.fieldOfView = camSize - (camDiffLowBounds * ((roadMiddleReferencePoint - Platform.instance.straightRoadLenght) / (roadMiddleReferencePoint - roadLengthLowerLimit)));
            }
            else // düz 60
            {
                Camera.main.fieldOfView = 60;
            }

            yield return new WaitForSeconds(0.001f);
        }
    }
}
