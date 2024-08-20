import React, { useEffect, useState } from 'react';
import { fetchMovieById } from '../services/Api.js';
import styles from '../styles/MovieDetails.module.css'

export default function MovieDetails(props) {
    const id  = props.id;
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

    const formatMovieLength = (length) => {
        const hours = Math.floor(length / 60);
        const minutes = (length % 60)
            .toString()
            .padStart(2, '0');

        return `${hours}ч ${minutes}мин`;
    }

    const formatRating = (rating) => {
        return (Math.round(rating * 10) / 10).toString().padEnd(3, ".0");
    }

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;

    return (
        <div className={styles.container}>
            <h1>{movie.name}</h1>
            <p className={styles.description}><b>Рейтинг: </b> {formatRating(movie.rate)}</p>
            <p className={styles.description}><b>Год: </b> {movie.year}</p>
            <p className={styles.description}><b>Жанры: </b> {movie.genres.join(', ')}</p>
            <p className={styles.description}><b>Длительность: </b> {formatMovieLength(movie.movieLength) }</p>
            <p className={styles.description}><b>Страны: </b> {movie.countries.join(', ')}</p>
            <button className={styles.wlb}>Посмотреть позже</button>
        </div>
    );
}