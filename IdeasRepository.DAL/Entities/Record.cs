using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeasRepository.DAL.Entities
{
    public class Record
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        public string TextBody { get; set; }

        public Guid RecordTypeId { get; set; }
        public virtual RecordType RecordType { get; set; }
    }
}
