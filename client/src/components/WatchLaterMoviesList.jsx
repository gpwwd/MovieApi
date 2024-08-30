import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { fetchUserLaterMovies } from "../services/Api.js";
import Movie from './Movie';
import styles from '../styles/MoviesList.module.css'

export default function WatchLaterMovies(props) {
    const [moviesWatchLater, setMovies] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
  
  
    useEffect(() => {
      const getWatchLaterMovies = async () => {
          try {
              const data = await fetchUserLaterMovies();
              setMovies(data);
          } catch (err) {
              setError(err);
          } finally {
              setLoading(false);
          }
      };
  
      getWatchLaterMovies();
    }, []);
  
  
    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;
    
    const listMovies = moviesWatchLater.map(movie => {
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
			<h2>Посмотреть позже</h2>
          	<ul className={styles.moviesList}> {listMovies} </ul>
      	</>
    );
}