import Movie from './Movie';
import {fetchMovies} from "../services/Api.js";
import React, { useEffect, useState } from 'react';
import "../App.css";

export default function MoviesList(props) {
    const [movies, setMovies] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
  
  
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
  
    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;
    
    const listMovies = movies.map(movie => 
        <Movie key={movie.id} name={movie.name} rating={movie.rate}/>
    );


    return (
      <ul className='movie_list'>
        {listMovies}
      </ul>
    );
}