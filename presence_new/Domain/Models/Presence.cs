using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.domain.Models
{
    public class Presence
    {

        public required User User { get; set; }
        public required int GroupId { get; set; }
        public bool IsAttedance { get; set; } = true;
        public required DateTime Date { get; set; }

        public required int LessonNumber { get; set; }
    }
}
