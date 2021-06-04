using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TqlPoWebApi.Models {
    public class Po {

        public static string StatusNew = "NEW";
        public static string StatusEdit = "EDIT";
        public static string StatusReview = "REVIEW";
        public static string StatusApproved = "APPROVED";
        public static string StatusRejected = "REJECTED";

        public int Id { get; set; }
        [Required, StringLength(80)]
        public string Description { get; set; }
        [Required, StringLength(20)]
        public string Status { get; set; } = Po.StatusNew;
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; } = 0;
        public bool Active { get; set; } = true;

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public Po() { }
    }
}
