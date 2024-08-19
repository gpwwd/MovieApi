import Movie from './Movie';
import {fetchUserLaterMovies} from "../services/Api.js";
import React, { useEffect, useState } from 'react';
import "../App.css";

export default function WatchLaterMovies(props) {
    const [moviesWatchLater, setMovies] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
  
  
    useEffect(() => {
      const getWatchLaterMovies = async () => {
          try {
              const data = await fetchUserLaterMovies("2b7db517-af90-4a15-a9da-b9b507e84627");
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
    
    const listMovies = moviesWatchLater.map(movie => 
        <Movie key={movie.id} name={movie.name} rating={movie.rate}/>
    );

    return (
      <ul className='movie_list'>
        {listMovies}
      </ul>
    );
}