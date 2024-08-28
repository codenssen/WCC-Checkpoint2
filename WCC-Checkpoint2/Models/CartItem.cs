namespace WCC_Checkpoint2.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
        public int BookId { get; set; }  // On stocke juste l'Id du livre

        public Book? Book { get; set; }         // Propriété de navigation vers le livre

        public int Quantity { get; set; }
        public decimal Price { get; set; }  // Le prix du livre au moment de l'ajout
    }
}
