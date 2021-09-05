using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Player time on the pitch
    /// </summary>
    public class FootballerPitchTime
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int AppearanceMinute { get; set; }

        public int MinutesPlayed { get; set; }

        public virtual Member Player { get; set; }
    }
}
