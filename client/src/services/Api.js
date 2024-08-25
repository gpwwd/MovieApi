const API_URL = "http://localhost:5128/api";

const fetchMovies = async () => {
    const response = await fetch(`${API_URL}/movies/images`);;
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

const fetchUserLaterMovies = async (id) => {
    const response = await fetch(`${API_URL}/users/${id}/watchList`, {
        method: 'GET',  // Specify the method, though it's GET by default
        headers: {
            'Authorization' : `Bearer` + `${localStorage.Authorization}`,
            'Content-Type': 'application/json', // Optional, depending on your API
        },
    });

    const data = await response.json();
    console.log("Watch Later movies" + data);

    return data;
}


export {fetchUserLaterMovies, fetchMovieById, fetchMovieImageUrlById, fetchMovies};