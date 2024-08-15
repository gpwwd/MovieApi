namespace MovieApiMvc.Models.DomainModels
{
    public class User
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string PasswHash { get; private set; }
        public string Email { get; private set; }
        public List<Movie> FavMovies { get; private set; }
        public List<Movie> WatchLaterMovies { get; private set; }

        private User(Guid id, string userName, string passwHash, string email, List<Movie>? favMovies, List<Movie>? watchLaterMovies)
        {
            Id = id;
            UserName = userName;
            PasswHash = passwHash;
            Email = email;
            FavMovies = favMovies ?? new List<Movie>();
            WatchLaterMovies = watchLaterMovies ?? new List<Movie>();
        }

        public static User Create(string userName, string passwHash, string email, List<Movie>? favMovies = null, List<Movie>? watchLaterMovies = null)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("User name must not be empty.", nameof(userName));

            if (string.IsNullOrWhiteSpace(passwHash))
                throw new ArgumentException("Password hash must not be empty.", nameof(passwHash));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email must not be empty.", nameof(email));

            return new User(
                id: Guid.NewGuid(),
                userName: userName,
                passwHash: passwHash,
                email: email,
                favMovies: favMovies,
                watchLaterMovies: watchLaterMovies
            );
        }
    }

}