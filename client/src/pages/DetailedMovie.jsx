import { useParams } from "react-router-dom";
import MovieDetails from "../components/MovieDetails";
import Header from "../components/Header";


export default function DetailedMovie() {
    const { id } = useParams();

    return (
        <>
            <header>
                <Header/>
            </header>
            <main>
                <MovieDetails id={id}/>
            </main>
        </>
    )
}