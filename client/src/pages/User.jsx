import Header from "../components/Header"
import WatchLaterMovies from "../components/WatchLaterMoviesList";

function User(props) {

	const handleLogOut = () => {
		localStorage.removeItem("authToken");
		localStorage.removeItem("email");
		window.location.href = '/';
	}
	
	return (
		<div>
			<header className="header">
				<Header/>
			</header>
			<main className="watchLaterUser">
				<WatchLaterMovies />
				<button onClick={handleLogOut}>Выйти</button>
			</main>
		</div>
	);
}

export default User;