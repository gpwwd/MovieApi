import { handleLogOut } from '../utils/handleLogOut';
import { Link } from 'react-router-dom';
import styles from '../styles/UserDropdown.module.css'

export default function UserDropdown(props) {
    return (
        <div className={styles.container}>
            <Link to='/user' className={styles.user}>{ props.email }</Link>       
            <div className={styles.content}>
                <div ><Link to='/user'>Смотреть позже</Link></div>
                <div ><Link to='#'>Изменить профиль</Link></div>
                <div className={styles.logOutOptionContainer}><Link to='/' className={styles.logOutOption} onClick={handleLogOut}>Выйти</Link></div>
            </div>
        </div>
    );
}