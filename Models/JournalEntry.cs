using System;
using System.ComponentModel.DataAnnotations;

namespace MyScriptureJournal.Models
{
    /// <summary>
    /// The JournalEntry class model
    /// </summary>
    public class JournalEntry
    {
        /// <summary>
        /// Gets or sets the id for the database
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the Title of the journal entry
        /// </summary>
        [StringLength(60, MinimumLength = 3), Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Notes on the entry
        /// </summary>
        [StringLength(100, MinimumLength = 5), Required]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the author's Book name
        /// </summary>
        [StringLength(25, MinimumLength = 3), Required]
        public string Book { get; set; }

        /// <summary>
        /// Gets or sets the journal Entry date
        /// </summary>
        [Display(Name = "Entry Date"), DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }
    }
}
