using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qr_reader.Models
{
    public class Article_type
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
