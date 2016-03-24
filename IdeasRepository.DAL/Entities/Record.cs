using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeasRepository.DAL.Entities
{
    public class Record
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        public string TextBody { get; set; }

        public string RecordTypeId { get; set; }
        public virtual RecordType RecordType { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
