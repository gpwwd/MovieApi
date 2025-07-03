const API_URL = "http://localhost:5128/api";

const fetchMovies = async () => {
    const response = await fetch(`${API_URL}/movies/get-with-images`);;
    return response.json();
};

const fetchMovieById = async (id) => {
    const response = await fetch(`${API_URL}/movies/${id}`);
    return response.json();
};

const fetchMovieImageUrlById = async (id) => {
    const response = await fetch(`${API_URL}/movies/${id}/images`);
    return response.json();
};

// export const fetchLogin = async (id) => {
//     const response = await fetch(`${API_URL}/movies/${id}`);
//     return response.json();
// };

const fetchUserLaterMovies = async () => {
    const response = await fetch(`${API_URL}/users/watchList`, {
        method: 'GET', 
        headers: {
            'Authorization' : `Bearer ` + `${localStorage.getItem('authToken')}`,
            'Content-Type': 'application/json',
        },
    });

    const data = await response.json();
    console.log("Watch Later movies" + data);

    return data;
}

export const addToWatchLater = async (movieId) => {
    const response = await fetch(`${API_URL}/users/addToWatchList`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ` + `${localStorage.getItem('authToken')}`,
        },
        body: JSON.stringify(movieId),
    });

    if (!response.ok) {
        throw new Error("Не удалось добавить фильм в список просмотра позже" + response.status);
    }

    return await response.json();
};

export {fetchUserLaterMovies, fetchMovieById, fetchMovieImageUrlById, fetchMovies};