namespace WCC_Checkpoint2.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }

        public Author? Author { get; set; }  // Propriété de navigation vers l'auteur

        public Category? Category { get; set; } // Propriété de navigation vers la catégorie

    }
}
