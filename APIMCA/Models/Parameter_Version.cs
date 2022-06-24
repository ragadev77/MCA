using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCA.Models
{
    [Table("parameter_version", Schema = "version")]
    public class Parameter_Version
    {
        public Parameter_Version() { }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int prv_id { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string prv_module { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string prv_version { get; set; }
        public DateTime prv_date { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string prv_status { get; set; }
        public int? prv_unique_parameter { get; set; } = -1;
        public DateTime? prv_sync_plan { get; set; }
        public int? prv_headerid { get; set; } = -1;

    }
}
