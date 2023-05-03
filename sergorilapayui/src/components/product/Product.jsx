import './Product.scss'

export default function Product({titleProduct, price, description, img}) {
    return (
        <div className="contentProduct">
            <img src={img} alt="productImg" className="productImg" />
            <h1 className="titleProduct">{titleProduct}</h1>
            <span className="descProduct">{description}</span>
            <b className="priceProduct">{price} руб</b>

        </div>
    );
    
}