import MoviesList from "../components/MoviesList";
import Header from "../components/Header"

function Home(props) {
  return (
    <div>
      <header className="header">
        <Header/>
      </header>
      <main className="mainHome">
        <MoviesList />
      </main>
    </div>
  );
}

export default Home;