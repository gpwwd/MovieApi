import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { fetchMovies } from "../services/Api.js";
import Movie from './Movie';
import styles from '../styles/MoviesList.module.css'
import { toast } from 'sonner';

export default function MoviesList(props) {
    const [movies, setMovies] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [searchTerm, setSearchTerm] = useState('');

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

    const handleSearchChange = (e) => {
        setSearchTerm(e.target.value);
    }

    const filteredMovies = searchTerm
        ? movies.filter(movie => movie.name.toLowerCase().includes(searchTerm.toLowerCase()))
        : movies;

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;

    const listMovies = filteredMovies.map(movie => {
        const imageUrl = movie.imageInfo.previewUrls && movie.imageInfo.previewUrls.length > 0 
            ? movie.imageInfo.previewUrls[0] 
            : null; 

        return (
            <Link to={`/movie/${movie.id}`} style={{textDecoration: "none"}}>
                <li key={movie.id}>
                    <Movie
                        name={movie.name}
                        rating={movie.ratingKp}
                        year={movie.year}
                        genres={movie.genres}
                        image={imageUrl}
                    />
                </li>
            </Link>
        );
    });

    return (
        <>
            <input className={styles.searchBar}
                type='text'
                placeholder='Поиск фильмов...'
                value={searchTerm}
                onChange={handleSearchChange}
            />
            <ul className={styles.moviesList}> {listMovies} </ul>
        </>
    );
}