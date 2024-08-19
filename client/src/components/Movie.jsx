import '../App.css'
import React, { useState } from 'react';
import "../App.css";

export default function Movie(props) {
    return (
        <div className="movie">
            <img src="https://ru.kinorium.com/1656658/gallery/poster/?photo=poster" alt="Green Book Poster" />
            <h2>{props.name}</h2>
            <h4>{props.rating}</h4>
        </div>
    );
}