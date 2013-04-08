using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TargetTapping.Back_end.Command
{
    class ChangeLevelCommand : CommandInterface
    {

        //the receiver object, our model (ie. the level) that will be the receiver of this command
        private Level ChangeLevelCommandsLevel;

        //new object data
        private Back_end.Object newObjectEntry;

        private Tuple<int, int> location = Tuple.Create(-1, -1);

        //old object data
        private Back_end.Object oldObjectEntry;



        //construcotr
        public ChangeLevelCommand(Level myLevel, Back_end.Object myObject)
        {
            //instantiate local access to the model
            this.ChangeLevelCommandsLevel = myLevel;
            //set the new object being added to the level.
            this.newObjectEntry = myObject;

            ///this.oldObjectEntry = myObject;

        }

        //Set the model to have the object that is passed in.
        public override void execute()
        {
            this.ChangeLevelCommandsLevel.addObject(this.newObjectEntry);

            
            //get the location of the object we just added in our list of lists
            for (int x = 0; x < this.ChangeLevelCommandsLevel.objectList.Count; ++x)
            {
                for (int y = 0; y < this.ChangeLevelCommandsLevel.objectList[x].Count; ++y)
                {
                    if (this.ChangeLevelCommandsLevel.objectList[x][y].Equals(this.newObjectEntry))
                    {
                        this.location = Tuple.Create(x, y);
                    }
                }
            }
            Console.WriteLine("added at: "+this.location.ToString());
            
            
        }

        //Set the model to have the previously stored object.
        public override void unexecute()
        {
            Console.WriteLine("removed at: "+this.location.ToString());
            //remove the object at the location.
            this.ChangeLevelCommandsLevel.objectList[this.location.Item1].RemoveAt(this.location.Item2);
           
        }
        //This method signature ensures all controllers implement is reversible.
        public override Boolean isReversible()
        {
            return true;
        }

          
    }
}
