using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoetryPlanet.Data.Models
{
    [Table("collection_quotes")]
    public class CollectionQuote
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("show_order")]
        public int ShowOrder { get; set; }

        [Column("quote_id")]
        public int QuoteId { get; set; }

        [Column("quote")]
        public string Quote { get; set; }= "";

        [Column("quote_author")]
        public string QuoteAuthor { get; set; }= "";

        [Column("quote_work")]
        public string QuoteWork { get; set; }= "";

        [Column("quote_work_id")]
        public int QuoteWorkId { get; set; }

        [Column("collection_id")]
        public int CollectionId { get; set; }

        [Column("collection_kind_id")]
        public int CollectionKindId { get; set; }

        [Column("quote_tr")]
        public string QuoteTr { get; set; }= "";

        [Column("quote_author_tr")]
        public string QuoteAuthorTr { get; set; }= "";

        [Column("quote_work_tr")]
        public string QuoteWorkTr { get; set; }= "";
    }
}