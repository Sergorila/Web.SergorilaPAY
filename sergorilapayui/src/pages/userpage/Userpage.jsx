import './Userpage.scss'
import Header from '../../components/header/Header';
import { useState } from 'react';
import { useEffect } from 'react';
import {userService} from '../../services/UserService'

export default function Userpage(){
    const login = localStorage.getItem('login');
    const [selectOption, setSelectOption] = useState([]);
    const [user, setUser] = useState([]);

    useEffect(() => {
        userService.getUser(login)
        .then((response) => {
            setUser(response);
            console.log(response);
        })
    }, []);

    return (
        <div className="container">
            <Header selectOption={selectOption} setSelectOption={setSelectOption} />
            <div className="userContent">
                <span className="userField">ФИО: {user.fio}</span>
                <span className="userField">Телефон: {user.phone}</span>
                <span className="userField">Telegram: @{user.telegramID}</span>
            </div>
        </div>
    );
}