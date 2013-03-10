using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OhTeamMachine.Widget
{
    /// <summary>
    /// A Tangible is a Widget that has a look and can be interacted with.
    /// </summary>
    /// A base tangible also assumes that 
    abstract class Tangible : Widget
    {

        public string TextureName { get; set; }

        public Tangible() : this("") { }

        public Tangible(string name)
        {
            TextureName = name;  
        
        }

        public override void prepare()
        {
            throw new NotImplementedException();
        }

    }
}
