using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.PdfVpMod.Enum;

namespace drz.PDFScaler
{
    /// <summary>
    /// настройки программы
    /// </summary>
    [Serializable]
    public class Setting
    {
        //todo вынести в settings
        WinGraphicsUnit _unit = WinGraphicsUnit.Millimeter;

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public WinGraphicsUnit Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
            }
        }

        ModeChangVp _mode = ModeChangVp.Add;

 
        public ModeChangVp Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
            }
        }

        bool _exitConfirmation=false;

        /// <summary>
        /// Gets or sets a value indicating whether [exit confirmation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exit confirmation]; otherwise, <c>false</c>.
        /// </value>
        public bool ExitConfirmation
        {
            get
            {
                return _exitConfirmation;
            }
            set
            {
                _exitConfirmation = value;
            }
        }

        bool _addBak = true;
        /// <summary>
        /// Gets or sets a value indicating whether [add backup].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add backup]; otherwise, <c>false</c>.
        /// </value>
        public bool AddBak
        {
            get
            {
                return _addBak;
            }
            set
            {
                _addBak = value;
            }
        }

        static Setting()
        {

        }


    }
}
