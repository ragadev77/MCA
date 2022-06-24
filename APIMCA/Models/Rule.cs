using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MCA.Models
{
    [Table("rule", Schema = "public")]
    public class Rule
    {
        public Rule() { }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int rul_id { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string rul_name { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string rul_desc { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string rul_condition { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string? rul_output { get; set; }
        public int rul_type { get; set; }
        public bool rul_is_active { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string rul_created_by { get; set; }
        [Column(TypeName = "varchar(50)")]
        [JsonIgnore]
        public string rul_modified_by { get; set; }
        [JsonIgnore]
        public bool? rul_is_deleted { get; set; } = false;
        [JsonIgnore]
        public bool rul_is_used { get; set; }
        public DateTime rul_created { get; set; }
        [Column(TypeName = "varchar(50)")]
        [JsonIgnore]
        public string rul_modified { get; set; }
        [Column(TypeName = "varchar(50)")]
        [JsonIgnore]
        public string rul_output_type { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string rul_approved_status { get; set; }
        [Column(TypeName = "varchar(50)")]
        [JsonIgnore]
        public string rul_approved_by { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string rul_category { get; set; }
        public int rul_id_ori { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string rul_version { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string rul_applied { get; set; }


    }
}
