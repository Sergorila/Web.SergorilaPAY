import { useEffect, useState } from 'react';
import './Header.scss'
import { Link } from 'react-router-dom';
import userimg from '../../assets/img/user_svg.svg'
import basketimg from '../../assets/img/basket.png'
import logoimg from '../../assets/img/logo.png'
import Select from 'react-select'
import UserSVG from '../UserSVG';
import BasketSVG from '../BasketSVG';
import {categoryService} from '../../services/CategoryService'

export default function Header({selectOption, setSelectOption}){
    const [options, setOptions] = useState([]);
    useEffect(() => {
            categoryService.getCategories()
            .then((response) => {
                const arr = response.map(item => ({label: item.title, value: item.id}));
                setSelectOption(arr[0]);
                setOptions(arr);
            })
        }, []);

    const [fillColor, setFillColor] = useState('#5fc4ec'); 
    return (
        <header>
            <div className="headerLeft">
                <Link to={'/'}>
                    <img className='logo' src={logoimg} alt='logo'/>
                </Link>
                <label>
                    Категории:
                    <select onChange={(e) => {
                        const label = e.target.value;
                        const option = options.find(item => item.label === label)
                        setSelectOption(option)
                        }}>
                        {options.map(item => (
                            <option value={item.label}>
                                {item.label}
                            </option>
                        ))}
                    </select>
                </label>
            </div>
            <div className="headerRight">
                <Link to='/user/:id'>
                    <UserSVG className='user' fillColor={fillColor} />
                </Link>
                <BasketSVG className='basket' fillColor={fillColor} />
            </div>
        </header>
    );
}