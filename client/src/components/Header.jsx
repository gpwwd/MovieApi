import { Link } from 'react-router-dom';
import styles from '../styles/Header.module.css'
import UserDropdown from './UserDropdown';

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
                <h1 className={styles.logo}>#bomb gay poit</h1>
                <div className={styles.navContainer}>
                    <div className={styles.navElementContainer}>
                        <Link to="/">Главная</Link>
                    </div>
                    <div className={styles.navElementContainer}>
                        <Link to="/movies">Фильмы</Link>
                    </div>
                </div>
            </div>
            <div className={styles.login_button_container}>
                {checkUserLoggedIn()
                    ? <UserDropdown email={localStorage.getItem('email')} />
                    : <Link to="/login"><button>Войти</button></Link>
                }

            </div>
        </header>
    );
}