using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeasRepository.DAL.Entities
{
    public class RecordType
    {
        public RecordType()
        {
            Records = new HashSet<Record>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Record> Records { get; set; }
    }
}
