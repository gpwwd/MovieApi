import { Link } from 'react-router-dom';
import styles from '../styles/Header.module.css'

export default function Header(props) {
    const checkUserLoggedIn = () => {
        const token = localStorage.getItem('authToken');
        
        if (token) {
            const payload = JSON.parse(atob(token.split('.')[1]));
            const isExpired = payload.exp * 1000 < Date.now(); 
            
            if (isExpired) {
                return false;
            }

            return true;
        } 
        
        return false;
    }

    return(
        <header className={styles.header}>
            <div className={styles.logoAndNav}>
                <h1>Название Сайта</h1>
                <nav>
                    <Link to="/">Главная</Link>
                    <Link to="/movies">Фильмы</Link>
                </nav>
            </div>
            <div className={styles.login_button_container}>
                {checkUserLoggedIn()
                    ? <Link to="/user">{localStorage.getItem("email")}</Link>
                    : <Link to="/login"><button>Войти</button></Link>
                }

            </div>
        </header>
    );
}