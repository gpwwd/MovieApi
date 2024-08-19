import { useParams } from "react-router-dom";
import MovieDetails from "../components/MovieDetails";


export default function DetailedMovie() {
    const { id } = useParams();

    return (
        <MovieDetails id={id}/>
    )
}