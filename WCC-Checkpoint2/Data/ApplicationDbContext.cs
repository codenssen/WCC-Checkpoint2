using Microsoft.EntityFrameworkCore;
using WCC_Checkpoint2.Models;

namespace WCC_Checkpoint2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItem>().Property(ci => ci.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CartItem>()
            .HasOne(ca => ca.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(car => car.CartId);

            // Seed des catégories
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Science-Fiction" },
                new Category() { Id = 2, Name = "Aventure" },
                new Category() { Id = 3, Name = "Fantastique" },
                new Category() { Id = 4, Name = "Horreur" },
                new Category() { Id = 5, Name = "Roman Historique" }
            );

            // Seed des auteurs
            modelBuilder.Entity<Author>().HasData(
                new Author()
                {
                    Id = 1,
                    Name = "James S.A. Corey",
                    Biography = "James S.A. Corey est le pseudonyme utilisé par les auteurs américains Daniel Abraham et Ty Franck pour écrire ensemble la série de science-fiction à succès The Expanse."
                },
                new Author()
                {
                    Id = 2,
                    Name = "Isaac Asimov",
                    Biography = "Isaac Asimov était un écrivain et biochimiste américain d'origine russe, célèbre pour ses œuvres de science-fiction, notamment les séries Fondation et Les Robots, ainsi que pour ses nombreuses contributions à la vulgarisation scientifique."
                },
                new Author()
                {
                    Id = 3,
                    Name = "J.R.R. Tolkien",
                    Biography = "J.R.R. Tolkien était un écrivain, poète, philologue et professeur d'université britannique, célèbre pour ses œuvres Le Hobbit et Le Seigneur des anneaux."
                },
                new Author()
                {
                    Id = 4,
                    Name = "Stephen King",
                    Biography = "Stephen King est un écrivain américain, maître de l'horreur, connu pour ses romans à succès comme Carrie, Shining, et Ça."
                },
                new Author()
                {
                    Id = 5,
                    Name = "Ken Follett",
                    Biography = "Ken Follett est un écrivain britannique spécialisé dans les thrillers historiques et les romans historiques, notamment la série Les Piliers de la terre."
                },
                new Author()
                {
                    Id = 6,
                    Name = "Dan Simmons",
                    Biography = "Dan Simmons est un écrivain américain de science-fiction, d'horreur et de fantastique, connu pour sa série Hyperion Cantos et ses œuvres mêlant mythologie, philosophie et aventures futuristes."
                },
                new Author()
                {
                    Id = 7,
                    Name = "Frank Herbert",
                    Biography = "Frank Herbert était un écrivain américain de science-fiction, surtout connu pour sa série Dune, qui est l'un des romans de science-fiction les plus vendus de tous les temps."
                }
            );

            // Seed des livres
            modelBuilder.Entity<Book>().HasData(
                new Book()
                {
                    Id = 1,
                    Title = "L'Eveil du Leviathan",
                    Price = 1.99,
                    Description = "L'Éveil du Léviathan est le premier tome de la série de science-fiction The Expanse, écrit par James S.A. Corey, qui mêle intrigue politique, mystère spatial, et aventure interplanétaire dans un futur où l'humanité a colonisé le système solaire.",
                    CoverImage = "/img/cover1.jpg",
                    ISBN = "9782253083658",
                    CategoryId = 1,
                    AuthorId = 1,
                },
                 new Book()
                 {
                     Id = 2,
                     Title = "Fondation Tome 1",
                     Price = 5.99,
                     Description = "Fondation Tome 1 est le début de la célèbre saga de science-fiction d'Isaac Asimov, qui raconte la création d'une fondation scientifique destinée à préserver le savoir de l'humanité et à réduire la durée d'un âge sombre inévitable dans un futur galactique.",
                     CoverImage = "/img/cover2.jpg",
                     ISBN = "9782207170212",
                     CategoryId = 1,
                     AuthorId = 2,
                 },
                 new Book()
                 {
                     Id = 3,
                     Title = "Le Seigneur des Anneaux : La Communauté de l'Anneau",
                     Price = 8.99,
                     Description = "Le premier tome de la célèbre saga fantastique de J.R.R. Tolkien, Le Seigneur des Anneaux, où un jeune hobbit est entraîné dans une quête pour détruire un anneau maléfique.",
                     CoverImage = "/img/cover3.jpg",
                     ISBN = "9782070501403",
                     CategoryId = 3,
                     AuthorId = 3,
                 },
                 new Book()
                 {
                     Id = 4,
                     Title = "Le Hobbit",
                     Price = 6.99,
                     Description = "Le Hobbit, ou Bilbo le Hobbit, est un roman fantastique de J.R.R. Tolkien qui raconte les aventures de Bilbo Baggins, un hobbit qui part en quête avec une troupe de nains pour récupérer un trésor gardé par un dragon.",
                     CoverImage = "/img/cover4.jpg",
                     ISBN = "9782266341233",
                     CategoryId = 3,
                     AuthorId = 3,
                 },
                 new Book()
                 {
                     Id = 5,
                     Title = "Shining",
                     Price = 7.99,
                     Description = "Shining est un roman d'horreur de Stephen King qui raconte l'histoire d'un écrivain, Jack Torrance, qui devient gardien d'un hôtel isolé où il sombre dans la folie.",
                     CoverImage = "/img/cover5.jpg",
                     ISBN = "9782253151623",
                     CategoryId = 4,
                     AuthorId = 4,
                 },
                 new Book()
                 {
                     Id = 6,
                     Title = "Ça",
                     Price = 9.99,
                     Description = "Ça est un roman d'horreur de Stephen King qui raconte l'histoire d'un groupe d'enfants confronté à une entité maléfique qui prend la forme d'un clown terrifiant.",
                     CoverImage = "/img/cover6.jpg",
                     ISBN = "9782253151340",
                     CategoryId = 4,
                     AuthorId = 4,
                 },
                 new Book()
                 {
                     Id = 7,
                     Title = "Les Piliers de la Terre",
                     Price = 10.99,
                     Description = "Les Piliers de la Terre est un roman historique de Ken Follett qui raconte la construction d'une cathédrale dans l'Angleterre médiévale et les luttes de pouvoir qui l'entourent.",
                     CoverImage = "/img/cover7.jpg",
                     ISBN = "9782253059530",
                     CategoryId = 5,
                     AuthorId = 5,
                 },
                 new Book()
                 {
                     Id = 8,
                     Title = "Un Monde Sans Fin",
                     Price = 12.99,
                     Description = "Un Monde Sans Fin est la suite des Piliers de la Terre de Ken Follett, et se déroule 200 ans plus tard, explorant les vies des descendants des personnages du premier livre.",
                     CoverImage = "/img/cover8.jpg",
                     ISBN = "9782253125761",
                     CategoryId = 5,
                     AuthorId = 5,
                 },
                 new Book()
                 {
                     Id = 9,
                     Title = "La Chute d'Hypérion",
                     Price = 11.99,
                     Description = "La Chute d'Hypérion est un roman de science-fiction de Dan Simmons, qui continue l'épopée spatiale commencée dans Hypérion, mêlant mythologie, philosophie, et aventure.",
                     CoverImage = "/img/cover9.jpg",
                     ISBN = "9782266285667",
                     CategoryId = 1,
                     AuthorId = 6,
                 },
                 new Book()
                 {
                     Id = 10,
                     Title = "Dune",
                     Price = 9.99,
                     Description = "Dune est un roman de science-fiction épique de Frank Herbert, qui se déroule dans un futur lointain où de grandes familles nobles se disputent la maîtrise de la planète désertique Arrakis.",
                     CoverImage = "/img/cover10.jpg",
                     ISBN = "9782266320481",
                     CategoryId = 1,
                     AuthorId = 7,
                 }
                );

            // Seed des paniers
            modelBuilder.Entity<Cart>().HasData(
                new Cart() { Id = 1 },  // Panier 1
                new Cart() { Id = 2 }   // Panier 2
            );

            // Seed des éléments du panier
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem()
                {
                    Id = 1,
                    CartId = 1,  // Relie à Cart 1
                    BookId = 1,  // L'Eveil du Leviathan
                    Quantity = 2,
                    Price = 1.99m
                },
                new CartItem()
                {
                    Id = 2,
                    CartId = 1,  // Relie à Cart 1
                    BookId = 3,  // Le Seigneur des Anneaux : La Communauté de l'Anneau
                    Quantity = 1,
                    Price = 8.99m
                },
                new CartItem()
                {
                    Id = 3,
                    CartId = 1,  // Relie à Cart 1
                    BookId = 5,  // Shining
                    Quantity = 1,
                    Price = 7.99m
                },
                new CartItem()
                {
                    Id = 4,
                    CartId = 2,  // Relie à Cart 2
                    BookId = 4,  // Le Hobbit
                    Quantity = 1,
                    Price = 6.99m
                },
                new CartItem()
                {
                    Id = 5,
                    CartId = 2,  // Relie à Cart 2
                    BookId = 8,  // Un Monde Sans Fin
                    Quantity = 1,
                    Price = 12.99m
                },
                new CartItem()
                {
                    Id = 6,
                    CartId = 2,  // Relie à Cart 2
                    BookId = 10,  // Dune
                    Quantity = 2,
                    Price = 9.99m
                }
            );

        }
    }
}
