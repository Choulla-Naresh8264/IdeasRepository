using System.Collections.Generic;

namespace IdeasRepository.DAL.Entities
{
    public class RecordType
    {
        public RecordType()
        {
            Records = new HashSet<Record>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Record> Records { get; set; }
    }
}
