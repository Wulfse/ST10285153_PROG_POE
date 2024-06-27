using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace PROG6221_Part3
{
    /// <summary>
    /// Class representing a step in the recipe
    /// </summary>
    ///-----------------------------------------------------------------------------------------------------------------///
    public class Step
    {
        //getter and setter for steps
        public string Description { get; set; }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="description"></param>
        ///------------------------------------------------------------------------------------------------------------///
        public Step(string description)
        {
            Description = description;
        }
    }
}
//-------------------------------------------------------End of Class---------------------------------------------------------//