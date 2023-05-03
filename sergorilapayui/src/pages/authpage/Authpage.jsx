import { useState } from 'react';
import './Authpage.scss'
import axios from 'axios'
import { useNavigate } from 'react-router-dom';

export default function Authpage(){
    const [registrationFlag, setRegistrationFlag] = useState(false);
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const [newLogin, setNewLogin] = useState("");
    const [newFIO, setNewFIO] = useState("");
    const [newPhone, setNewPhone] = useState("");
    const [newTelegramId, setNewTelegramId] = useState("");
    
    const navigate = new useNavigate();

    const loginFunction = () => {
        axios.get('http://localhost:5001/api/User/Login', {
            params: {
                "login": login,
                "password": password
            }
        }).then((res) => {
            if (res.status === 200) {
                localStorage.setItem("login", login);
                navigate('/', {state:{ name: login}} );
            } 
        }).catch(() => {
            alert('Ошибка авторизации');
        })
    }

    const registrationFunction = () => {
        axios.post('http://localhost:5001/api/User', {
            "login": newLogin,
            "password": "21313453463564563",
            "fio": newFIO,
            "phone": newPhone,
            "telegramID": newTelegramId
        }).then((res) => {
            if (res.status === 200) {
                setRegistrationFlag(false);
            } 
        }).catch(() => {
            alert('Ошибка регистрации');
        })
    }

    
    return (
        <div className="containerAuth">
            <div className="authContent">
                {!registrationFlag
                    ? <>
                    Login
                    <input name="myInput" value={login} onInput={(e) => setLogin(e.target.value)} />
                    Password
                    <input name="myInput" value={password} onInput={(e) => setPassword(e.target.value)} />
                    <button onClick={() => setRegistrationFlag(true)}>Зарегистрироваться</button>
                    <button onClick={loginFunction}>Войти</button>
                    </>
                    : <>
                    Login
                    <input name="myInput" value={newLogin} onInput={(e) => setNewLogin(e.target.value)} />
                    FIO
                    <input name="myInput" value={newFIO} onInput={(e) => setNewFIO(e.target.value)} />
                    Phone
                    <input name="myInput" value={newPhone} onInput={(e) => setNewPhone(e.target.value)} />
                    TelegramID
                    <input name="myInput" value={newTelegramId} onInput={(e) => setNewTelegramId(e.target.value)} />
                    <button onClick={registrationFunction}>Зарегистрироваться</button>
                    </>
                }
                
            </div>
        </div>
    );
}