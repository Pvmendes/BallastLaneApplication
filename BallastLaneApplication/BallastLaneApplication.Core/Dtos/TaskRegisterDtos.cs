using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallastLaneApplication.Core.Dtos
{
    public class TaskRegisterDtos
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Enum.TaskStatus Status { get; set; }
    }
}
