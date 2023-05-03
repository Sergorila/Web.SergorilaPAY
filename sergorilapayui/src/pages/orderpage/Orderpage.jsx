import './Userpage.scss'
import Header from '../../components/header/Header';
import { useState } from 'react';
import { useEffect } from 'react';

export default function Orderpage(){
    const login = localStorage.getItem('login');
    const [selectOption, setSelectOption] = useState([]);
    const [user, setUser] = useState([]);

    return (
        <div className="container">
            <Header selectOption={selectOption} setSelectOption={setSelectOption} />
            <div className="orderContent">

            </div>
        </div>
    );
}