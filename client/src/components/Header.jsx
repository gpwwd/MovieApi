import { Link } from 'react-router-dom';
import styles from '../styles/Header.module.css'

export default function Header(props) {
    return(
        <header className={styles.header}>
            <div className={styles.logo}>
                <h1>Название Сайта</h1>
                <nav>
                    <Link to="/">Главная</Link>
                    <Link to="/movies">Фильмы</Link>
                </nav>
            </div>
            <div className={styles.login_button_container}>
                <button>Войти</button>
            </div>
        </header>
    );
}