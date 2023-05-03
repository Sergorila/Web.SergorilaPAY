import './Category.scss'
import Product from "../product/Product"
import {categoryService} from '../../services/CategoryService'
import { useState, useEffect } from 'react';

export default function Category({titleCategory, selectOption}) {
    const [products, setProducts] = useState([]);
    useEffect(() => {
        categoryService.getCategoryItems(selectOption.value, 0, 100)
        .then((response) => {
            const arr = response.map(item => ({id: item.id, title: item.title, price: item.price, img: item.img, description: item.description}));
            setProducts(arr);
        })
    }, [selectOption]);

    useEffect(() => {
    }, [products])
    return (
        <div className="content">
            <h1 className="title">{titleCategory}</h1>
            <div className="listproducts">
            {products.map(product => (
                <Product titleProduct={product.title} price={product.price} description={product.description} img={product.img}/>
            ))}
            </div>
            
        </div>
    );
    
}