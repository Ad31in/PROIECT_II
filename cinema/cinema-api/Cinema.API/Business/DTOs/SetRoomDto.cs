using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class SetRoomDto
    {
        [Required] public int Number { get; set; }

        [Required] public int Capacity { get; set; }
    }
}
