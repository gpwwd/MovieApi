import MoviesList from "../components/MoviesList";
import Header from "../components/Header"

function Home(props) {
	return (
		<>
			<header className="header">
				<Header name="мужик"/>
			</header>
			<main className="mainHome">
				<MoviesList />
			</main>
			<footer>
				Jopa        
			</footer>
		</>
	);
}

export default Home;