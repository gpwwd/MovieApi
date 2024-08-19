const API_URL = "http://localhost:5128/api";

const fetchMovies = async () => {
    const response = await fetch(`${API_URL}/movies`);;
    return response.json();
};

const fetchMovieById = async (id) => {
    const response = await fetch(`${API_URL}/movies/${id}`);
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
            'Authorization' : 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJodHRwczovL2pveWRpcGthbmppbGFsLmNvbS8iLCJhdWQiOiJodHRwczovL2pveWRpcGthbmppbGFsLmNvbS8ifQ.Pi92hSkahkc3uIMia8zsphtucrRrUGE1jWF3PQHpKQa5_8MZIYRj84aJeA_yAjFCnL5ZTO7DLeu7nR3B-fhIfA',
            'Content-Type': 'application/json', // Optional, depending on your API
        },
    });

    const data = await response.json();
    console.log("Watch Later movies" + data);

    return data;
}


export {fetchUserLaterMovies, fetchMovieById, fetchMovies};