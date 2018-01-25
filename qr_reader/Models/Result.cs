using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace qr_reader.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        [StringLength(700)]
        public string Results { get; set; }

        //public virtual ICollection<Unit> units { get; set; }
        public int UnitId  { get; set; }
        public virtual Unit Unit { get; set; }

        [ForeignKey("Test")]
        public int? TestId { get; set; }
        public virtual Test Test { get; set; }
    }
}
