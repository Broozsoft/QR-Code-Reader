using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace qr_reader.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [StringLength(500)]
        public string Property { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Datetime { get; set; }
        public virtual ICollection<Result> Results { get; set; }

        [ForeignKey("article_type")]
        public int Article_typeId { get; set; }
        public virtual Article_type Article_type { get; set; }
    }
}
