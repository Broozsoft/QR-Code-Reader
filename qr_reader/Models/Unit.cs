using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace qr_reader.Models
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public int SerialNumber { get; set; }

        [ForeignKey("article")]
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

        [ForeignKey("order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        //public virtual ICollection<Result> result { get; set; }
        public virtual ICollection<Result> Results{ get; set; }
    }
}
