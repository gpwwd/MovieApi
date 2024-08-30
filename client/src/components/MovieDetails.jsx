import React, { useEffect, useState } from 'react';
import { fetchMovieById, fetchMovieImageUrlById } from '../services/Api.js';
import { handleWatchLater  } from '../utils/handleWatchLater';
import styles from '../styles/MovieDetails.module.css';

export default function MovieDetails(props) {
    const id  = props.id;
    const [movie, setMovie] = useState(null);
    const [image, setImage] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    
    useEffect(() => {
        const getMovieData = async () => {
            setLoading(true); // Устанавливаем загрузку в true в начале
            try {
                const [movieData, imageData] = await Promise.all([
                    fetchMovieById(id),
                    fetchMovieImageUrlById(id),
                ]);
                setMovie(movieData);
                setImage(imageData);
            } catch (err) {
                setError(err);
            } finally {
                setLoading(false); // Устанавливаем загрузку в false в конце
            }
        };

        getMovieData();
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

    console.log(movie);

    const imageUrl = image.urls && image.urls.length > 0 
        ? image.urls[0] 
        : null; 

    return (
        <div className={styles.container}>
            <h1>{movie.name}</h1>
            <h3>{movie.alternativeName}</h3>
            <div className={styles.descriptionAndImageContainer}>
                {imageUrl ? <img className={styles.image} src={imageUrl}></img> : <div/>}
                <div className={styles.infoContainer}>
                    {movie.ratingKp ? <p className={styles.info}><b>Рейтинг Кинопоиска: </b> {formatRating(movie.ratingKp)}</p> : <></>}
                    {movie.ratingImdb ? <p className={styles.info}><b>Рейтинг IMDb: </b> {formatRating(movie.ratingImdb)}</p> : <></>}
                    {movie.ratingFilmCritics ? <p className={styles.info}><b>Рейтинг FilmCritics: </b> {formatRating(movie.ratingFilmCritics)}</p> : <></>}
                    <p className={styles.info}><b>Год: </b> {movie.year}</p>
                    <p className={styles.info}><b>Жанры: </b> {movie.genres.join(', ')}</p>
                    <p className={styles.info}><b>Длительность: </b> {formatMovieLength(movie.movieLength) }</p>
                    <p className={styles.info}><b>Страны: </b> {movie.countries.join(', ')}</p>
                </div>
            </div>
            <div className={styles.descriptionContainer}>
                <h2>Про что фильм «{movie.name}»:</h2>
                <p>{movie.description}</p>
            </div>
            <button className={styles.watchLaterButton} onClick={ () => handleWatchLater(id)}>
                Посмотреть позже
            </button>
        </div>
    );
}