import Header from "../components/Header"
import WatchLaterMovies from "../components/WatchLaterMoviesList";
import { handleLogOut } from "../utils/handleLogOut";

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