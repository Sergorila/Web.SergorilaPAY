import './Mainpage.scss'
import Header from '../../components/header/Header';
import Category from '../../components/category/Category';
import { Link } from 'react-router-dom';
import { useState } from 'react';

export default function Mainpage(){
    const [selectOption, setSelectOption] = useState([]);
    return (
        <div className="container">
            <Header selectOption={selectOption} setSelectOption={setSelectOption} />
            <Category titleCategory={selectOption.label} selectOption={selectOption}/>
        </div>
    );
}