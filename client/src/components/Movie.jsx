import React, { useState } from 'react';
import styles from "../styles/Movie.module.css"

export default function Movie(props) {

    const formatRating = (rating) => {
        return (Math.round(rating * 10) / 10).toString().padEnd(3, ".0");
    }

    return (
        <div className={styles.movie}>
            {props.image
                ? (<img className={styles.poster} src={props.image} alt={`${props.name} poster`} />)
                : (<div className={styles.placeholder}></div>)
            }
            <h2 className={styles.title}>{props.name}</h2>
            <h4>Рейтинг: {formatRating(props.rating)}</h4>
            <h4>Год: {props.year}</h4>
            <h4>Жанры: {props.genres.join(', ')}</h4>
        </div>
    );
}