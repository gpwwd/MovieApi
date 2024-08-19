import '../App.css'

export default function Header(props) {
    return(
        <header className= "header">
            <ul className="header_buttons_wrapper">
                <li>
                    <h1>Hello, {props.name}</h1>
                </li>
                <li>О сайте</li>
                <li>Фильмы</li>
            </ul>
            <button className='login_button'></button>
        </header>
    );
}