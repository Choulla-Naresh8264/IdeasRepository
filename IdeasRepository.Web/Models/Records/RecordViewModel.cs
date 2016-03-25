using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdeasRepository.Web.Models.Records
{
    public class RecordViewModel
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string CreationDate { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string TextBody { get; set; }

        public string RecordType { get; set; }

        [Required]
        public string RecordTypeId { get; set; }

        public bool IsDeleted { get; set; }
    }
}