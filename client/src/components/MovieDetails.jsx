import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchMovieById } from '../services/Api.js';

export default function MovieDetail() {
    const { id } = useParams();
    const [movie, setMovie] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const getMovie = async () => {
            try {
                const data = await fetchMovieById(id);
                setMovie(data);
            } catch (err) {
                setError(err);
            } finally {
                setLoading(false);
            }
        };

        getMovie();
    }, [id]);

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;

    return (
        <div>
            <h1>{movie.name}</h1>
            <h4>Рейтинг: {movie.rating}</h4>
            <h4>Год: {movie.year}</h4>
            <h4>Жанры: {movie.genres.join(', ')}</h4>
            {/* Добавьте другие детали фильма здесь */}
        </div>
    );
}