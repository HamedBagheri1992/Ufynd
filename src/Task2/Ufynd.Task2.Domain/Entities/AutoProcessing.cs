using System;

namespace Ufynd.Task2.Domain.Entities
{
    public class AutoProcessing
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public DateTime SendTime { get; set; }
        public string FileAddress { get; set; }
        public bool IsSend { get; set; }
    }
}
