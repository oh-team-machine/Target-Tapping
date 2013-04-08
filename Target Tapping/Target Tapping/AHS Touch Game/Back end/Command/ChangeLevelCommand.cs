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
        //private List<List<TargetTapping.Back_end.Object>> oldLevelList;
        //private List<List<TargetTapping.Back_end.Object>> newLevelList;
        //private bool objectAddedAlready;
        private Tuple<int, int> location = Tuple.Create(-1, -1);

        //old object data
        //private Back_end.Object oldObjectEntry;



        //construcotr
        public ChangeLevelCommand(Level myLevel, Back_end.Object myObject)
        {
            //instantiate local access to the model
            this.ChangeLevelCommandsLevel = myLevel;
            //set the new object being added to the level.
            this.newObjectEntry = myObject;

            //this.oldLevelList = new List<List<TargetTapping.Back_end.Object>>();
            //this.newLevelList = new List<List<TargetTapping.Back_end.Object>>();
            //this.objectAddedAlready = false;
            ///this.oldObjectEntry = myObject;

        }

        //Set the model to have the object that is passed in.
        public override void execute()
        {
            ////make a copy of the current level and set it to oldLevelList
            //for (int x = 0; x < this.ChangeLevelCommandsLevel.objectList.Count; ++x)
            //{
            //    this.oldLevelList.Add(new List<TargetTapping.Back_end.Object>(this.ChangeLevelCommandsLevel.objectList[x]));
                    
            //}
            
            ////add the new object to it the current list
            ////only do this once though.
            //if (this.objectAddedAlready == false)
            //{
                this.ChangeLevelCommandsLevel.addObject(this.newObjectEntry);
                //make a copy of the current level and set it to newLevelList
                
            //    for (int x = 0; x < this.ChangeLevelCommandsLevel.objectList.Count; ++x)
            //    {
            //        this.newLevelList.Add(new List<TargetTapping.Back_end.Object>(this.ChangeLevelCommandsLevel.objectList.ElementAt(x)));

            //    }
            //    this.objectAddedAlready = true;

            //}
            //else
            //{
            //    Console.WriteLine("setting to newlist");
            //    this.ChangeLevelCommandsLevel.objectList = this.newLevelList;
            //}
            

            

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
                Console.WriteLine("added at: " + this.location.ToString());
        }

        //Set the model to have the previously stored object.
        public override void unexecute()
        {
            Console.WriteLine("removed at: "+this.location.ToString());
            //remove the object at the location.
            this.ChangeLevelCommandsLevel.objectList[this.location.Item1].RemoveAt(this.location.Item2);
            //Console.WriteLine("setting to oldlist");
            //this.ChangeLevelCommandsLevel.objectList = this.oldLevelList;


           
        }
        //This method signature ensures all controllers implement is reversible.
        public override Boolean isReversible()
        {
            return true;
        }

          
    }
}
