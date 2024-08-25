import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { fetchMovies } from "../services/Api.js";
import Movie from './Movie';

export default function MoviesList(props) {
    const [movies, setMovies] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate(); // Используем useNavigate

    useEffect(() => {
        const getMovies = async () => {
            try {
                const data = await fetchMovies();
                setMovies(data);
            } catch (err) {
                setError(err);
            } finally {
                setLoading(false);
            }
        };

        getMovies();
    }, []);

    const handleMovieClick = (id) => {
        navigate(`/movie/${id}`); 
    };

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;

    const listMovies = movies.map(movie => {
        const imageUrl = movie.imageInfo.previewUrls && movie.imageInfo.previewUrls.length > 0 
            ? movie.imageInfo.previewUrls[0] 
            : null; 

        return (
            <li key={movie.id} onClick={() => handleMovieClick(movie.id)}>
                <Movie
                    name={movie.name}
                    rating={movie.ratingKp}
                    year={movie.year}
                    genres={movie.genres}
                    image={imageUrl}
                />
            </li>
        );
    });

    return (
        <ul className='movie_list'>
            {listMovies}
        </ul>
    );
}