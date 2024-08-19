import Header from "../components/Header"
import WatchLaterMovies from "../components/FavouriteMoviesList";

function User(props) {
  return (
    <div>
      <header className="header">
        <Header/>
      </header>
      <main className="watchLaterUser">
        <WatchLaterMovies />
      </main>
    </div>
  );
}

export default User;