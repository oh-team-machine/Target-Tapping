using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TargetTapping.Back_end
{
    class Level
    {
        public Level () {
            objectList = new List<List<Object>>();
            currentPosition = 0;
            multiSelect = false;
            upTime = 0;
            holdTime = 0;
        }

        public List<List<TargetTapping.Back_end.Object>> objectList { get; set; }
        public int currentPosition { get; set; }
        public bool multiSelect { get; set; }
        public int upTime { get; set; }
        public int holdTime { get; set; }
        public string levelName { get; set; }

        public void addObject(TargetTapping.Back_end.Object objectPassed)
        {
            List<TargetTapping.Back_end.Object> placeHolder = new List<TargetTapping.Back_end.Object>();
            placeHolder.Add(objectPassed);
            if (multiSelect == true)
            {
                objectList.Add(placeHolder);
            }
            else if (currentPosition == 0)
            {
                objectList.Add(placeHolder);
            }
            else
            {
                currentPosition = currentPosition + 1;
                objectList.Add(placeHolder);
            }
        }

        public void removeObject()
        {
            int lastObjectPosition = -1;
            foreach (var objectB in objectList[currentPosition])
        	{
                lastObjectPosition = lastObjectPosition + 1;
        	}
            if (lastObjectPosition == 0)
            {
                objectList[currentPosition].RemoveAt(lastObjectPosition);
                currentPosition = currentPosition - 1;
            }
            else
            {
                objectList[currentPosition].RemoveAt(lastObjectPosition);
            }
        }
    }
}
