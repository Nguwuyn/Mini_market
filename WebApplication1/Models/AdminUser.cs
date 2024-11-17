namespace WebApplication1.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AdminUser")]
    public partial class AdminUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string UserName { get; set; }

        public int? CustomerID { get; set; }
    }
}
