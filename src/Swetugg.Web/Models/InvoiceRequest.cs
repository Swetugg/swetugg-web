using System.ComponentModel.DataAnnotations;

namespace Swetugg.Web.Models
{
    public class InvoiceRequest
    {
        public int Id { get; set; }
        public int ConferenceId { get; set; }

        [StringLength(250)]
        [Required]
        [Display(Name = "Namn (för- och efternamn)")]
        public string InvoiceReciver { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-post")]
        public string Email { get; set; }

        [StringLength(250)]
        [Required]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [StringLength(250)]
        [Required]
        [Display(Name = "Företag")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(13)]
        [Display(Name = "Organisationsnummer")]
        public string OrganisationNumber { get; set; }


        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Fakturaadress")]
        public string InvoiceAddress { get; set; }

        [Required]
        [StringLength(6)]
        [Display(Name = "Postnummer")]
        public string PostNumber { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Ort")]
        public string PostCity { get; set; }


        [StringLength(250)]
        [Display(Name = "Märk fakturan med")]
        public string InvoiceNote { get; set; }


        [EmailAddress]
        [Required]
        [Display(Name = "Epost att skicka fakturan till")]
        public string InvoiceEmail { get; set; }

        [Range(5, 20)]
        [Required]
        [Display(Name = "Antal biljetter")]
        public int NumberOfTickets { get; set; }

        [StringLength(250)]
        [Display(Name = "Meddelande till Swetugg")]
        public string Note { get; set; }
    }
}
