using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TargetTapping.Back_end.Command
{
    //Interface for sub command object to  abstract the 3 methods below.
       abstract public class CommandInterface
    {
        //The method signature ensures all controllers implement execute.
        abstract public void execute();
        //This method signature ensures all controllers implement un-execute.
        abstract public void unexecute();
        //This method signature ensures all controllers implement is reversible.
        abstract public Boolean isReversible();
    }

}
