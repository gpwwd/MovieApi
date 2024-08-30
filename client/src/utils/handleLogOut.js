
export const handleLogOut = () => {
    localStorage.removeItem("authToken");
    localStorage.removeItem("email");
    window.location.href = '/';
}