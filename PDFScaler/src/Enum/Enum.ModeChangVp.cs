using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drz.Abstractions.Interfaces
{
    /// <summary>
    /// Режим изменения VP PDF
    /// </summary>
    enum ModeChangVp
    {
        /// <summary>
        /// Добавить, не менять существующий
        /// </summary>
        Add,

        /// <summary>
        /// Добавить, изменить существующий
        /// </summary>
        AddOrMod,

        /// <summary>
        /// Удалить
        /// </summary>
        Del
    }
}
