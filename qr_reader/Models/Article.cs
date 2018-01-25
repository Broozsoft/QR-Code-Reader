using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace qr_reader.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string ArticleNumber { get; set; }
        public string Uplimit { get; set; }
        public string Lowlimit { get; set; }

        [ForeignKey("article_type")]
        public int Article_typeId { get; set; }
        public virtual Article_type Article_type { get; set; }

        public virtual ICollection<Unit> Units { get; set; }
    }
}
